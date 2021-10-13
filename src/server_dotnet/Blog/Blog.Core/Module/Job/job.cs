using Sixpence.EntityFramework.Entity;
using System;
using System.Runtime.Serialization;

namespace Blog.Core.Module.Job
{
    [Entity("job", "作业", true)]
    public class job : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("jobid", "实体id", DataType.Varchar, 100)]
        public string jobId
        {
            get
            {
                return this.Id;
            }
            set
            {
                this.Id = value;
            }
        }

        /// <summary>
        /// 执行计划
        /// </summary>
        [DataMember]
        [Attr("runtime", "执行计划", DataType.Varchar, 100)]
        public string runTime { get; set; }

        /// <summary>
        /// 上次运行时间
        /// </summary>
        [DataMember]
        [Attr("lastruntime", "上次运行时间", DataType.Timestamp, 27)]
        public DateTime? lastRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        [DataMember]
        [Attr("nextruntime", "下次运行时间", DataType.Timestamp, 27)]
        public DateTime? nextRunTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        [Attr("description", "描述", DataType.Varchar, 400)]
        public string description { get; set; }
    }
}