using Sixpence.ORM.Entity;
using System;
using System.Runtime.Serialization;

namespace Blog.Core.Module.Job
{
    public class job
    {
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string name { get; set; }

        public long? prev_fire_time_ticks { get; set; }

        /// <summary>
        /// 上次运行时间
        /// </summary>
        [DataMember]
        public DateTime? prev_fire_time
        {
            get
            {
                if (this.prev_fire_time_ticks.HasValue)
                {
                    return new DateTime(this.prev_fire_time_ticks.Value).ToLocalTime();
                }
                return null;
            }
        }

        public long? next_fire_time_ticks { get; set; }


        /// <summary>
        /// 下次运行时间
        /// </summary>
        [DataMember]
        public DateTime? next_fire_time
        {
            get
            {
                if (this.next_fire_time_ticks.HasValue)
                {
                    return new DateTime(this.next_fire_time_ticks.Value).ToLocalTime();
                }
                return null;
            }
        }

        public string trigger_state { get; set; }

        [DataMember]
        public string status
        {
            get
            {
                switch (this.trigger_state)
                {
                    case "WAITING":
                        return "等待";
                    case "ACQUIRED":
                        return "正常执行";
                    case "PAUSED":
                        return "暂停";
                    case "ERROR":
                        return "错误";
                    case "BLOCKED":
                        return "阻塞";
                    default:
                        return null;
                }
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        public string description { get; set; }

        [DataMember]
        public string cron_expression { get; set; }
    }
}