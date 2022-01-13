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
        [Column("name", "名称", DataType.Varchar, 100)]
        public string name { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [DataMember]
        [Column("parentid", "上级菜单id", DataType.Varchar, 100)]
        public string parentid { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [DataMember]
        [Column("parentidname", "上级菜单名", DataType.Varchar, 100)]
        public string parentIdName { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [DataMember]
        [Column("router", "路由地址", DataType.Varchar, 200)]
        public string router { get; set; }

        /// <summary>
        /// 菜单索引
        /// </summary>
        [DataMember]
        [Column("menu_index", "菜单索引", DataType.Int4)]
        public int? menu_Index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        [Column("statecode", "状态", DataType.Int4)]
        public int? stateCode { get; set; }


        [DataMember]
        [Column("statecodename", "状态", DataType.Varchar, 100)]
        public string stateCodeName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DataMember]
        [Column("icon", "图标", DataType.Varchar, 100)]
        public string icon { get; set; }
    }
}