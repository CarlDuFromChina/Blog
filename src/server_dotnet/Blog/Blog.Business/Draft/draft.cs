using Blog.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Draft
{
    [Entity("draft", "草稿", false)]
    public partial class draft : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        [Attr("draftid", "草稿id", AttrType.Varchar, 100)]
        public string draftId
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
        /// 博客id
        /// </summary>
        private string _blogid;
        [DataMember]
        [Attr("blogid", "博客id", AttrType.Varchar, 100)]
        public string blogId
        {
            get
            {
                return this._blogid;
            }
            set
            {
                this._blogid = value;
                SetAttributeValue("blogId", value);
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        private string _content;
        [DataMember]
        [Attr("content", "内容", AttrType.Text)]
        public string content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
                SetAttributeValue("content", value);
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        private string _title;
        [DataMember]
        [Attr("title", "标题", AttrType.Varchar, 100)]
        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
                SetAttributeValue("title", value);
            }
        }
    }
}
