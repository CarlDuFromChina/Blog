using Newtonsoft.Json.Linq;
using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Blog.Comments
{
    [EntityName("comments")]
    public class comments : BaseEntity
    {
        private string _commentsid;
        [DataMember]
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
