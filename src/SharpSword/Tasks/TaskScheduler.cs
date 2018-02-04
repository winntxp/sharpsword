/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/8/2016 10:35:49 AM
 * ****************************************************************/
using System;

namespace SharpSword.Tasks
{
    /// <summary>
    /// 调取器描述类
    /// </summary>
    [Serializable]
    public class TaskScheduler : IEquatable<TaskScheduler>, IComparable, IComparable<TaskScheduler>
    {
        /// <summary>
        /// 调度器友好的名称(全局唯一)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 多少秒执行一次
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// ITask作业任务的类型
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// 调度器是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 当遇到错误的时候，是否停止执行
        /// </summary>
        public bool StopOnError { get; set; }

        /// <summary>
        /// 在分布式站点中，确保一个作业任务只运行一个实例，防止并发重复作业
        /// </summary>
        public bool RunOnOneWebFarmInstance { get; set; }

        /// <summary>
        /// 重写下相等判断
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(TaskScheduler other)
        {
            if (other.IsNull())
            {
                return false;
            }
            return this.Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 比较两个指定的 TaskScheduler 对象，并返回一个指示二者在排序顺序中的相对位置的整数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>一个 32 位带符号整数，指示两个比较数之间的词法关系</returns>
        public int CompareTo(object obj)
        {
            if (obj.IsNull())
            {
                return 1;
            }

            if (!(obj is TaskScheduler))
            {
                throw new ArgumentException("可比较对象obj必须为TaskScheduler类型");
            }

            return this.CompareTo((TaskScheduler)obj);
        }

        /// <summary>
        /// 比较两个指定的 TaskScheduler 对象，并返回一个指示二者在排序顺序中的相对位置的整数。
        /// </summary>
        /// <param name="other"></param>
        /// <returns>一个 32 位带符号整数，指示两个比较数之间的词法关系</returns>
        public int CompareTo(TaskScheduler other)
        {
            if (other.IsNull())
            {
                return 1;
            }

            return string.Compare(this.Name, other.Name);
        }
    }
}
