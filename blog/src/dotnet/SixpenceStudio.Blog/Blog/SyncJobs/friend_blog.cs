
using SixpenceStudio.Platform.Entity;
using System;
using System.Runtime.Serialization;


namespace SixpenceStudio.Blog.Blog.SyncJobs
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
        /// 创建时间
        /// </summary>
        private DateTime? _createdOn;
        [DataMember]
        public DateTime? createdOn
        {
            get
            {
                return this._createdOn;
            }
            set
            {
                this._createdOn = value;
                SetAttributeValue("createdOn", value);
            }
        }


        /// <summary>
        /// 修改时间
        /// </summary>
        private DateTime? _modifiedOn;
        [DataMember]
        public DateTime? modifiedOn
        {
            get
            {
                return this._modifiedOn;
            }
            set
            {
                this._modifiedOn = value;
                SetAttributeValue("modifiedOn", value);
            }
        }


    }
}

