using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Blog.Business.Link
{
    [Entity("link", "链接")]
    public class link : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember]
        [Column("link_url", "链接地址", DataType.Text)]
        public string link_url { get; set; }

        /// <summary>
        /// 链接类型
        /// </summary>
        [DataMember]
        [Column("link_type", "链接类型", DataType.Varchar, 100)]
        public string link_type { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Column("brief", "摘要", DataType.Varchar, 100)]
        public string brief { get; set; }
    }
}
