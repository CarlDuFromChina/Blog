using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Draft
{
    [EntityName("draft")]
    public partial class draft : BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        private string _draftid;
        [DataMember]
        public string draftId
        {
            get
            {
                return this._draftid;
            }
            set
            {
                this._draftid = value;
                SetAttributeValue("draftId", value);
            }
        }

        /// <summary>
        /// 博客id
        /// </summary>
        private string _blogid;
        [DataMember]
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
