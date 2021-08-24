using Newtonsoft.Json.Linq;
using Blog.Core.Data;
using System;
using System.Runtime.Serialization;


namespace Blog.Comments
{
    [Entity("comments", "评论", false)]
    public partial class comments : BaseEntity
    {
        [DataMember]
        [Attr("commentsid", "评论id", AttrType.Varchar, 100)]
        public string commentsId
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
        /// 评论
        /// </summary>
        private string _comment;
        [DataMember]
        [Attr("comment", "评论", AttrType.Text)]
        public string comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
                SetAttributeValue("comment", value);
            }
        }

        /// <summary>
        /// 实体Id
        /// </summary>
        private string _objectid;
        [DataMember]
        [Attr("objectid", "实体Id", AttrType.Varchar, 100)]
        public string objectId
        {
            get
            {
                return this._objectid;
            }
            set
            {
                this._objectid = value;
                SetAttributeValue("objectId", value);
            }
        }

        /// <summary>
        /// 类型名
        /// </summary>
        private string _object_name;
        [DataMember]
        [Attr("object_name", "类型名", AttrType.Varchar, 100)]
        public string object_name
        {
            get
            {
                return this._object_name;
            }
            set
            {
                this._object_name = value;
                SetAttributeValue("object_name", value);
            }
        }

        private string _parentid;
        /// <summary>
        /// 父节点
        /// </summary>
        [DataMember]
        [Attr("parentid", "父节点", AttrType.Varchar, 100)]
        public string parentid
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
                SetAttributeValue("parentid", value);
            }
        }

        private string _replyid;
        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember]
        [Attr("replyid", "回复人", AttrType.Varchar, 100)]
        public string replyid
        {
            get
            {
                return this._replyid;
            }
            set
            {
                this._replyid = value;
                SetAttributeValue("replyid", value);
            }
        }

        private string _replyidName;
        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember]
        [Attr("replyidName", "回复人", AttrType.Varchar, 100)]
        public string replyidName
        {
            get
            {
                return this._replyidName;
            }
            set
            {
                this._replyidName = value;
                SetAttributeValue("replyidName", value);
            }
        }
    }
}
