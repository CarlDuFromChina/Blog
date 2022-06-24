using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.idea
{
    [Entity("idea", "想法", false)]
    public partial class idea : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        [Column]
        public string content { get; set; }
    }
}
