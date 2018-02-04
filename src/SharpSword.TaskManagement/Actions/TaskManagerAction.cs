/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/6/12 16:37:34
 * ****************************************************************/
using SharpSword.Tasks;
using SharpSword.WebApi;
using System;
using System.Collections.Generic;

namespace SharpSword.TaskManagement.Actions
{
    /// <summary>
    /// 作业任务管理器
    /// </summary>
    [ActionName("Api.TaskManager"), DisableDataSignatureTransmission, DisablePackageSdk, EnableRecordApiLog(true), AllowAnonymous]
    public class TaskManagerAction : ActionBase<TaskManagerAction.TaskManagerActionRequestDto, List<TaskManagerAction.TaskThreadDto>>
    {
        /// <summary>
        /// 上送的参数对象
        /// </summary>
        public class TaskManagerActionRequestDto : RequestDtoBase
        {
            /// <summary>
            /// 自定义校验上送参数是否正确
            /// </summary>
            /// <returns></returns>
            public override IEnumerable<DtoValidatorResultError> Valid()
            {
                return base.Valid();
            }
        }

        /// <summary>
        /// 作业任务工作线程传输DTO
        /// </summary>
        [Serializable]
        public class TaskThreadDto
        {
            /// <summary>
            /// 每间隔多少秒执行一次
            /// </summary>
            public int Seconds { get; set; }

            /// <summary>
            /// 启动时间
            /// </summary>
            public DateTime Started { get; set; }

            /// <summary>
            /// 最后执行时间
            /// </summary>
            public DateTime LastRuned { get; set; }

            /// <summary>
            /// 是否正在运行
            /// </summary>
            public bool IsRunning { get; set; }

            /// <summary>
            /// 所有任务集合
            /// </summary>
            public List<TaskDto> Tasks { get; set; }

        }

        /// <summary>
        /// 作业任务传输DTO
        /// </summary>
        [Serializable]
        public class TaskDto
        {
            /// <summary>
            /// 调度器友好的名称
            /// </summary>
            public string Name { get; set; }

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
            /// 是否正在运行
            /// </summary>
            public bool IsRunning { get; set; }

            /// <summary>
            /// 是否只能在一个实例上运行
            /// </summary>
            public bool RunOnOneWebFarmInstance { get; set; }

            /// <summary>
            /// 作业调度器再特定运行实例里运行的到期时间
            /// 租约到期时间(一般默认租约到期时间为30天)
            /// </summary>
            public DateTime? LeasedUntil { get; set; }

            /// <summary>
            /// 作业调度器再特东运行实例里运行的实例名称
            /// 租约机器（在分布式执行的时候，因为每个IIS都会有一套还行任务，
            /// 因此我们先签约第一个执行的机器来执行，其他分布式机器比较此租
            /// 约机器名称来判断是否执行）
            /// </summary>
            public string LeasedByMachineName { get; set; }
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<List<TaskThreadDto>> Execute()
        {
            var responseDto = new List<TaskThreadDto>();

            //获取所有的作业任务工作线程
            var taskThreads = TaskThreadManager.Instance.TaskThreads;

            foreach (var taskThread in taskThreads)
            {
                var taskThreadDto = new TaskThreadDto()
                {
                    Seconds = taskThread.Seconds,
                    IsRunning = taskThread.IsRunning,
                    Started = taskThread.Started,
                    LastRuned = taskThread.LastRuned,
                    Tasks = new List<TaskDto>()
                };

                //获取工作线程下所有的作业任务
                foreach (var task in taskThread.Tasks)
                {
                    taskThreadDto.Tasks.Add(new TaskDto()
                    {
                        Enabled = task.Enabled,
                        Name = task.Name,
                        StopOnError = task.StopOnError,
                        TaskType = task.TaskType,
                        IsRunning = task.IsRunning,
                        RunOnOneWebFarmInstance = task.RunOnOneWebFarmInstance,
                        LeasedUntil = task.LeasedUntil,
                        LeasedByMachineName = task.LeasedByMachineName
                    });
                }

                responseDto.Add(taskThreadDto);
            }

            return this.SuccessActionResult(responseDto);
        }
    }
}
