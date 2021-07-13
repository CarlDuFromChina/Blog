using Blog.Core.Data;
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
        [Attr("jobid", "实体id", AttrType.Varchar, 100)]
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
        private string _runtime;
        [DataMember]
        [Attr("runtime", "执行计划", AttrType.Varchar, 100)]
        public string runTime
        {
            get
            {
                return _runtime;
            }
            set
            {
                _runtime = value;
                SetAttributeValue("runTime", value);
            }
        }

        /// <summary>
        /// 上次运行时间
        /// </summary>
        private DateTime? _lastRunTime;
        [DataMember]
        [Attr("lastruntime", "上次运行时间", AttrType.Timestamp, 27)]
        public DateTime? lastRunTime
        {
            get
            {
                return _lastRunTime;
            }
            set
            {
                _lastRunTime = value;
                SetAttributeValue("lastRunTime", value);
            }
        }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        private DateTime? _nextRunTime;
        [DataMember]
        [Attr("nextruntime", "下次运行时间", AttrType.Timestamp, 27)]
        public DateTime? nextRunTime
        {
            get
            {
                return _nextRunTime;
            }
            set
            {
                _nextRunTime = value;
                SetAttributeValue("nextRunTime", value);
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        private string _description;
        [DataMember]
        [Attr("description", "描述", AttrType.Varchar, 400)]
        public string description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                SetAttributeValue("description", value);
            }
        }
    }
}