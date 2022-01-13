using Sixpence.ORM.Entity;
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
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 执行计划
        /// </summary>
        [DataMember]
        [Column("runtime", "执行计划", DataType.Varchar, 100)]
        public string runTime { get; set; }

        /// <summary>
        /// 上次运行时间
        /// </summary>
        [DataMember]
        [Column("lastruntime", "上次运行时间", DataType.Timestamp, 27)]
        public DateTime? lastRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        [DataMember]
        [Column("nextruntime", "下次运行时间", DataType.Timestamp, 27)]
        public DateTime? nextRunTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        [Column("description", "描述", DataType.Varchar, 400)]
        public string description { get; set; }
    }
}