using Blog.Core.Data;
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

        private string _link_url;
        /// <summary>
        /// 链接地址
        /// </summary>
        [DataMember]
        [Attr("link_url", "链接地址", AttrType.Text)]
        public string link_url
        {
            get
            {
                return this._link_url;
            }
            set
            {
                this._link_url = value;
                SetAttributeValue("link_url", value);
            }
        }

        private string _link_type;
        /// <summary>
        /// 链接类型
        /// </summary>
        [DataMember]
        [Attr("link_type", "链接类型", AttrType.Varchar, 100)]
        public string link_type
        {
            get
            {
                return this._link_type;
            }
            set
            {
                this._link_type = value;
                SetAttributeValue("link_type", value);
            }
        }

        private string _brief;
        /// <summary>
        /// 摘要
        /// </summary>
        [DataMember]
        [Attr("brief", "摘要", AttrType.Varchar, 100)]
        public string brief
        {
            get
            {
                return this._brief;
            }
            set
            {
                this._brief = value;
                SetAttributeValue("brief", value);
            }
        }
    }
}
