using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Entity
{
    [Entity("idea", "想法")]
    public partial class Idea : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember, PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember, Column, Description("内容")]
        public string content { get; set; }
    }
}
