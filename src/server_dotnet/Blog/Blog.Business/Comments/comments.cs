using Sixpence.ORM.Entity;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;


namespace Blog.Comments
{
    [Entity("comments", "评论", false)]
    public partial class comments : BaseEntity
    {
        [DataMember, PrimaryColumn, Description("id")]
        public string id { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        [DataMember, Column, Description("评论")]
        public string comment { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 评论类型
        /// </summary>
        [DataMember, Column, Description("评论类型")]
        public string comment_type { get; set; }

        /// <summary>
        /// 实体Id
        /// </summary>
        [DataMember, Column, Description("实体Id")]
        public string objectId { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        [DataMember, Column, Description("类型名")]
        public string object_name { get; set; }

        /// <summary>
        /// 对象标题
        /// </summary>
        [DataMember, Column, Description("对象标题")]
        public string object_title { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember, Column, Description("对象拥有者")]
        public string object_ownerid { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember, Column, Description("对象拥有者")]
        public string object_ownerid_name { get; set; }

        /// <summary>
        /// 父节点
        /// </summary>
        [DataMember, Column, Description("父节点")]
        public string parentid { get; set; }

        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember, Column, Description("回复人")]
        public string replyid { get; set; }

        /// <summary>
        /// 回复人
        /// </summary>
        [DataMember, Column, Description("回复人")]
        public string replyid_name { get; set; }
    }
}
