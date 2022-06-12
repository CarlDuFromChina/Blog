using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Blog.Core.Module.JobHisotry
{
    [Entity("job_history", "作业执行记录", false)]
    public class job_history : BaseEntity
    {
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 作业名
        /// </summary>
        [DataMember]
        [Column]
        public string job_name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        [Column]
        public DateTime? start_time { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        [Column]
        public DateTime? end_time { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        [Column]
        public string status { get; set; }

        /// <summary>
        /// 错误原因
        /// </summary>
        [DataMember]
        [Column]
        public string error_msg { get; set; }
    }
}
