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
        [Column("comment", "评论", DataType.Text)]
        public string comment { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 评论类型
        /// </summary>
        [DataMember]
        [Column("comment_type", "评论类型", DataType.Varchar)]
        public string comment_type { get; set; }

        /// <summary>
        /// 实体Id
        /// </summary>
        [DataMember]
        [Column("objectid", "实体Id", DataType.Varchar, 100)]
        public string objectId { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        [DataMember]
        [Column("object_name", "类型名", DataType.Varchar, 100)]
        public string object_name { get; set; }

        /// <summary>
        /// 对象标题
        /// </summary>
        [DataMember]
        [Column("object_title", "对象标题", DataType.Varchar, 100)]
        public string object_title { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Column("object_ownerid", "对象拥有者", DataType.Varchar, 100)]
        public string object_ownerid { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Column("object_ownerid_name", "对象拥有者", DataType.Varchar, 100)]
        public string object_ownerid_name { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        [DataMember]
        [Column("parentid", "父节点", DataType.Varchar, 100)]
        public string parentid { get; set; }

        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember]
        [Column("replyid", "回复人", DataType.Varchar, 100)]
        public string replyid { get; set; }

        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember]
        [Column("replyid_name", "回复人", DataType.Varchar, 100)]
        public string replyid_name { get; set; }
    }
}
