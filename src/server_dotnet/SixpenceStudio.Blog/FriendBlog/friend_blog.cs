using SixpenceStudio.Core.Entity;
using System;
using System.Runtime.Serialization;

namespace SixpenceStudio.Blog.FriendBlog
{
    [EntityName("friend_blog")]
    public partial class friend_blog : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        private string _friend_blogid;
        [DataMember]
        public string friend_blogId
        {
            get
            {
                return this._friend_blogid;
            }
            set
            {
                this._friend_blogid = value;
                SetAttributeValue("friend_blogId", value);
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
        /// 图片地址
        /// </summary>
        private string _first_picture;
        [DataMember]
        public string first_picture
        {
            get
            {
                return this._first_picture;
            }
            set
            {
                this._first_picture = value;
                SetAttributeValue("first_picture", value);
            }
        }


        /// <summary>
        /// 作者
        /// </summary>
        private string _author;
        [DataMember]
        public string author
        {
            get
            {
                return this._author;
            }
            set
            {
                this._author = value;
                SetAttributeValue("author", value);
            }
        }


        /// <summary>
        /// 描述
        /// </summary>
        private string _description;
        [DataMember]
        public string description
        {
            get
            {
                return this._description;
            }
            set
            {
                this._description = value;
                SetAttributeValue("description", value);
            }
        }

    }
}

