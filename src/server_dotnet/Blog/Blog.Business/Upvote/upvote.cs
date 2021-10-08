using Sixpence.EntityFramework.Entity;
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
        [Attr("upvoteid", "点赞id", AttrType.Varchar, 100)]
        public string upvoteId
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
        /// 对象Id
        /// </summary>
        [DataMember]
        [Attr("objectid", "实体Id", AttrType.Varchar, 100)]
        public string objectId { get; set; }

        /// <summary>
        /// 对象名
        /// </summary>
        [DataMember]
        [Attr("objectidname", "实体名", AttrType.Varchar, 100)]
        public string objectIdName { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Attr("object_ownerid", "对象拥有者", AttrType.Varchar, 100)]
        public string object_ownerid { get; set; }

        /// <summary>
        /// 对象拥有者
        /// </summary>
        [DataMember]
        [Attr("object_owneridName", "对象拥有者", AttrType.Varchar, 100)]
        public string object_owneridName { get; set; }

        /// <summary>
        /// 点赞类型
        /// </summary>
        [DataMember]
        [Attr("object_type", "实体Id", AttrType.Varchar, 100)]
        public string object_type { get; set; }
    }
}
