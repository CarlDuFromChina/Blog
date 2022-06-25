using Sixpence.ORM.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DataMember, Column, Description("名称")]
        public string name { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [DataMember, Column, Description("上级菜单")]
        public string parentid { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [DataMember, Column, Description("上级菜单")]
        public string parentId_name { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [DataMember, Column, Description("路由地址")]
        public string router { get; set; }

        /// <summary>
        /// 菜单索引
        /// </summary>
        [DataMember, Column, Description("菜单索引")]
        public int? menu_index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember, Column, Description("状态")]
        public bool? statecode { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        [DataMember, Column, Description("状态名称")]
        public string statecode_name { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DataMember, Column, Description("图标")]
        public string icon { get; set; }
    }
}