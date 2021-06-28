using Newtonsoft.Json.Linq;
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Blog.Comments
{
    [Entity("comments", "评论", false)]
    public class comments : BaseEntity
    {
        private string _commentsid;
        [DataMember]
        [Attr("commentsid", "评论id", AttrType.Varchar, 100)]
        public string commentsId
        {
            get
            {
                return this._commentsid;
            }
            set
            {
                this._commentsid = value;
                SetAttributeValue("commentsId", value);
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
    }
}
