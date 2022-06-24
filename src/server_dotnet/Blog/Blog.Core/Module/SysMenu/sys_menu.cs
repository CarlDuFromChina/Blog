using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Blog.Core.Module.SysMenu
{
    [Entity("sys_menu", "系统菜单",  true)]
    public partial class sys_menu : BaseEntity
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
        [Column]
        public string name { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [DataMember]
        [Column]
        public string parentid { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [DataMember]
        [Column]
        public string parentId_name { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [DataMember]
        [Column]
        public string router { get; set; }

        /// <summary>
        /// 菜单索引
        /// </summary>
        [DataMember]
        [Column]
        public int? menu_index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        [Column]
        public bool? statecode { get; set; }


        [DataMember]
        [Column]
        public string statecode_name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DataMember]
        [Column]
        public string icon { get; set; }
    }
}