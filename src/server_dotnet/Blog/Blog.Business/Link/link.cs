using Sixpence.EntityFramework.Entity;
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
        [Attr("linkid", "链接id", AttrType.Varchar, 100, true)]
        public string linkId
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
        /// 链接地址
        /// </summary>
        [DataMember]
        [Attr("link_url", "链接地址", AttrType.Text)]
        public string link_url { get; set; }

        /// <summary>
        /// 链接类型
        /// </summary>
        [DataMember]
        [Attr("link_type", "链接类型", AttrType.Varchar, 100)]
        public string link_type { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Attr("brief", "摘要", AttrType.Varchar, 100)]
        public string brief { get; set; }
    }
}
