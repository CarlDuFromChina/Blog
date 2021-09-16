using Sixpence.EntityFramework.Entity;
using System;
using System.Runtime.Serialization;

namespace Blog.FriendBlog
{
    [Entity("friend_blog", "友人博客", false)]
    public partial class friend_blog : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [Attr("friend_blogid", "友人博客id", AttrType.Varchar, 100)]
        public string friend_blogId
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
        /// 内容
        /// </summary>
        [DataMember]
        [Attr("content", "内容", AttrType.Text)]
        public string content { get; set; }


        /// <summary>
        /// 图片地址
        /// </summary>
        [DataMember]
        [Attr("first_picture", "图片地址", AttrType.Varchar, 500)]
        public string first_picture { get; set; }


        /// <summary>
        /// 作者
        /// </summary>
        [DataMember]
        [Attr("author", "作者", AttrType.Varchar, 100)]
        public string author { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [DataMember]
        [Attr("description", "描述", AttrType.Varchar, 400)]
        public string description { get; set; }
    }
}

