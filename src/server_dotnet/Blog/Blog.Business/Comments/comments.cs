using Newtonsoft.Json.Linq;
using Sixpence.EntityFramework.Entity;
using System;
using System.Runtime.Serialization;


namespace Blog.Comments
{
    [Entity("comments", "评论", false)]
    public partial class comments : BaseEntity
    {
        [DataMember]
        [Attr("commentsid", "评论id", DataType.Varchar, 100)]
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
        [DataMember]
        [Attr("comment", "评论", DataType.Text)]
        public string comment { get; set; }

        /// <summary>
        /// 评论类型
        /// </summary>
        [DataMember]
        [Attr("comment_type", "评论类型", DataType.Varchar)]
        public string comment_type { get; set; }

        /// <summary>
        /// 实体Id
        /// </summary>
        [DataMember]
        [Attr("objectid", "实体Id", DataType.Varchar, 100)]
        public string objectId { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        [DataMember]
        [Attr("object_name", "类型名", DataType.Varchar, 100)]
        public string object_name { get; set; }

        /// <summary>
        /// 对象标题
        /// </summary>
        [DataMember]
        [Attr("object_title", "对象标题", DataType.Varchar, 100)]
        public string object_title { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Attr("object_ownerid", "对象拥有者", DataType.Varchar, 100)]
        public string object_ownerid { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Attr("object_owneridName", "对象拥有者", DataType.Varchar, 100)]
        public string object_owneridName { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        [DataMember]
        [Attr("parentid", "父节点", DataType.Varchar, 100)]
        public string parentid { get; set; }

        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember]
        [Attr("replyid", "回复人", DataType.Varchar, 100)]
        public string replyid { get; set; }

        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember]
        [Attr("replyidName", "回复人", DataType.Varchar, 100)]
        public string replyidName { get; set; }
    }
}
