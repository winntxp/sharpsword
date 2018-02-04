#region LICENSE
/*
 *   Copyright 2014 Angelo Simone Scotto <scotto.a@gmail.com>
 * 
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 * 
 *       http://www.apache.org/licenses/LICENSE-2.0
 * 
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 * 
 * */
#endregion

using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SharpSword.DistributedLock.Redis
{
    /// <summary>
    /// 使用单例模式
    /// </summary>
    internal class RedisLock
    {
        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<string, ConnectionMultiplexer> redisMasterDictionary = new Dictionary<string, ConnectionMultiplexer>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="servers">初始化多个redis复用器</param>
        public RedisLock(params ConnectionMultiplexer[] servers)
        {
            foreach (var item in servers)
            {
                this.redisMasterDictionary.Add(item.GetEndPoints().First().ToString(), item);
            }
        }

        /// <summary>
        /// 我们只重试3次（3次还未拿到锁，我们直接返回加锁失败）
        /// </summary>
        const int DefaultRetryCount = 2;

        /// <summary>
        /// 随机重试的毫秒数
        /// </summary>
        readonly TimeSpan DefaultRetryDelay = new TimeSpan(0, 0, 0, 0, 130);

        /// <summary>
        /// 
        /// </summary>
        const double ClockDriveFactor = 0.01;

        /// <summary>
        /// 只要过半节点没有问题，分布式锁就可以正常工作
        /// </summary>
        protected int Quorum => (redisMasterDictionary.Count / 2) + 1;

        /// <summary>
        /// String containing the Lua unlock script.
        /// </summary>
        const string UnlockScript = @"if redis.call(""get"",KEYS[1]) == ARGV[1] then
                                            return redis.call(""del"",KEYS[1])
                                      else
                                            return 0
                                      end";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected static byte[] CreateUniqueLockId()
        {
            return Guid.NewGuid().ToByteArray();
        }

        /// <summary>
        /// TODO: Refactor passing a ConnectionMultiplexer
        /// </summary>
        /// <param name="redisServer"></param>
        /// <param name="resource"></param>
        /// <param name="val"></param>
        /// <param name="ttl"></param>
        /// <returns></returns>
        protected bool LockInstance(string redisServer, string resource, byte[] val, TimeSpan ttl)
        {
            bool succeeded;
            try
            {
                var redis = this.redisMasterDictionary[redisServer];
                succeeded = redis.GetDatabase().StringSet(resource, val, ttl, When.NotExists);
            }
            catch (Exception)
            {
                succeeded = false;
            }
            return succeeded;
        }

        /// <summary>
        /// TODO: Refactor passing a ConnectionMultiplexer
        /// </summary>
        /// <param name="redisServer"></param>
        /// <param name="resource"></param>
        /// <param name="val"></param>
        protected void UnlockInstance(string redisServer, string resource, byte[] val)
        {
            RedisKey[] key = { resource };
            RedisValue[] values = { val };
            try
            {
                var redis = redisMasterDictionary[redisServer];
                redis.GetDatabase().ScriptEvaluate(UnlockScript, key, values);
            }
            catch { }
        }

        /// <summary>
        /// 加锁
        /// </summary>
        /// <param name="resource">资源名称</param>
        /// <param name="ttl">锁超时时间（即：锁就算是没有释放，到期自动释放，方式其他人都操作不了数据了，死锁）</param>
        /// <param name="lockObject">返回锁资源信息</param>
        /// <returns></returns>
        public bool Lock(RedisKey resource, TimeSpan ttl, out Lock lockObject)
        {
            var val = CreateUniqueLockId();
            Lock innerLock = null;
            bool successfull = retry(DefaultRetryCount, DefaultRetryDelay, () =>
            {
                try
                {
                    int n = 0;
                    var startTime = DateTime.Now;

                    // Use keys
                    for_each_redis_registered(redis =>
                    {
                        if (LockInstance(redis, resource, val, ttl))
                        {
                            n += 1;
                        }
                    });

                    /*
                     * Add 2 milliseconds to the drift to account for Redis expires
                     * precision, which is 1 millisecond, plus 1 millisecond min drift 
                     * for small TTLs.        
                     */
                    var drift = Convert.ToInt32((ttl.TotalMilliseconds * ClockDriveFactor) + 2);
                    var validity_time = ttl - (DateTime.Now - startTime) - new TimeSpan(0, 0, 0, 0, drift);

                    if (n >= Quorum && validity_time.TotalMilliseconds > 0)
                    {
                        innerLock = new Lock(resource, val, validity_time);
                        return true;
                    }
                    else
                    {
                        for_each_redis_registered(redis =>
                        {
                            UnlockInstance(redis, resource, val);
                        });

                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            });

            lockObject = innerLock;

            return successfull;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        protected void for_each_redis_registered(Action<ConnectionMultiplexer> action)
        {
            foreach (var item in redisMasterDictionary)
            {
                action(item.Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        protected void for_each_redis_registered(Action<string> action)
        {
            foreach (var item in redisMasterDictionary)
            {
                action(item.Key);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="retryCount"></param>
        /// <param name="retryDelay"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected bool retry(int retryCount, TimeSpan retryDelay, Func<bool> action)
        {
            int maxRetryDelay = (int)retryDelay.TotalMilliseconds;
            Random rnd = new Random();
            int currentRetry = 0;
            while (currentRetry++ < retryCount)
            {
                if (action())
                {
                    return true;
                }
                Thread.Sleep(rnd.Next(maxRetryDelay));
            }
            return false;
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        /// <param name="lockObject"></param>
        public void Unlock(Lock lockObject)
        {
            for_each_redis_registered(redis =>
            {
                UnlockInstance(redis, lockObject.Resource, lockObject.Value);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.GetType().FullName);
            sb.AppendLine("Registered Connections:");
            foreach (var item in redisMasterDictionary)
            {
                sb.AppendLine(item.Value.GetEndPoints().First().ToString());
            }
            return sb.ToString();
        }
    }
}
