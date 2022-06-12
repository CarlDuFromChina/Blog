using Newtonsoft.Json.Linq;
using Sixpence.ORM.Entity;
using System;
using System.Runtime.Serialization;


namespace Blog.Comments
{
    [Entity("comments", "评论", false)]
    public partial class comments : BaseEntity
    {
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        [DataMember]
        [Column]
        public string comment { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 评论类型
        /// </summary>
        [DataMember]
        [Column]
        public string comment_type { get; set; }

        /// <summary>
        /// 实体Id
        /// </summary>
        [DataMember]
        [Column]
        public string objectId { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        [DataMember]
        [Column]
        public string object_name { get; set; }

        /// <summary>
        /// 对象标题
        /// </summary>
        [DataMember]
        [Column]
        public string object_title { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Column]
        public string object_ownerid { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Column]
        public string object_ownerid_name { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        [DataMember]
        [Column]
        public string parentid { get; set; }

        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember]
        [Column]
        public string replyid { get; set; }

        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember]
        [Column]
        public string replyid_name { get; set; }
    }
}
