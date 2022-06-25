﻿using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 对象Id
        /// </summary>
        [DataMember, Column, Description("对象Id")]
        public string objectId { get; set; }

        /// <summary>
        /// 对象名
        /// </summary>
        [DataMember, Column, Description("对象名")]
        public string objectid_name { get; set; }

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
        /// 点赞类型
        /// </summary>
        [DataMember, Column, Description("点赞类型")]
        public string object_type { get; set; }
    }
}
