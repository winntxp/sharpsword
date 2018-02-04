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

namespace SharpSword.DistributedLock.Redis
{
    internal class Lock
    {
        private RedisKey resource;
        private RedisValue val;
        private TimeSpan validity_time;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="val"></param>
        /// <param name="validity"></param>
        public Lock(RedisKey resource, RedisValue val, TimeSpan validity)
        {
            this.resource = resource;
            this.val = val;
            this.validity_time = validity;
        }

        /// <summary>
        /// 资源名称
        /// </summary>
        public RedisKey Resource { get { return resource; } }

        /// <summary>
        /// 资源值
        /// </summary>
        public RedisValue Value { get { return val; } }

        /// <summary>
        /// 获取锁后留给业务逻辑的执行时间（超过这个时间锁会自动解锁）
        /// </summary>
        public TimeSpan Validity { get { return validity_time; } }
    }
}
