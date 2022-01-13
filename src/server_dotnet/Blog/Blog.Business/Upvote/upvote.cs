using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Blog.Business.Upvote
{
    [Entity("upvote", "点赞")]
    public class upvote : BaseEntity
    {
        /// <summary>
        /// 实体id
        /// </summary>
        [DataMember]
        [PrimaryColumn]
        public string id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 对象Id
        /// </summary>
        [DataMember]
        [Column("objectid", "实体Id", DataType.Varchar, 100)]
        public string objectId { get; set; }

        /// <summary>
        /// 对象名
        /// </summary>
        [DataMember]
        [Column("objectidname", "实体名", DataType.Varchar, 100)]
        public string objectIdName { get; set; }

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
        [Column("object_owneridName", "对象拥有者", DataType.Varchar, 100)]
        public string object_owneridName { get; set; }

        /// <summary>
        /// 点赞类型
        /// </summary>
        [DataMember]
        [Column("object_type", "实体Id", DataType.Varchar, 100)]
        public string object_type { get; set; }
    }
}
