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

        [DataMember]
        [Column("job_name", "作业名", DataType.Varchar, 100, true)]
        public string job_name { get; set; }

        [DataMember]
        [Column("start_time", "开始时间", DataType.Timestamp, true)]
        public DateTime? start_time { get; set; }
        
        [DataMember]
        [Column("end_time", "结束时间", DataType.Timestamp, true)]
        public DateTime? end_time { get; set; }

        [DataMember]
        [Column("status", "状态", DataType.Varchar, 100, true)]
        public string status { get; set; }

        [DataMember]
        [Column("error_msg", "错误原因", DataType.Text)]
        public string error_msg { get; set; }
    }
}
