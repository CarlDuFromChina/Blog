using Blog.Core.Data;
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
        [Attr("sys_menuid", "实体id", AttrType.Varchar, 100)]
        public string sys_menuId
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
        /// 上级菜单
        /// </summary>
        [DataMember]
        [Attr("parentid", "上级菜单id", AttrType.Varchar, 100)]
        public string parentid { get; set; }

        /// <summary>
        /// 上级菜单
        /// </summary>
        [DataMember]
        [Attr("parentidname", "上级菜单名", AttrType.Varchar, 100)]
        public string parentIdName { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        [DataMember]
        [Attr("router", "路由地址", AttrType.Varchar, 200)]
        public string router { get; set; }

        /// <summary>
        /// 菜单索引
        /// </summary>
        [DataMember]
        [Attr("menu_index", "菜单索引", AttrType.Int4)]
        public int? menu_Index { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        [Attr("statecode", "状态", AttrType.Int4)]
        public int? stateCode { get; set; }


        [DataMember]
        [Attr("statecodename", "状态", AttrType.Varchar, 100)]
        public string stateCodeName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DataMember]
        [Attr("icon", "图标", AttrType.Varchar, 100)]
        public string icon { get; set; }
    }
}