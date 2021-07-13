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
        private string _parentid;
        [DataMember]
        [Attr("parentid", "上级菜单id", AttrType.Varchar, 100)]
        public string parentid
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
                SetAttributeValue("parentid", value);
            }
        }

        /// <summary>
        /// 上级菜单
        /// </summary>
        private string _parentidname;
        [DataMember]
        [Attr("parentidname", "上级菜单名", AttrType.Varchar, 100)]
        public string parentIdName
        {
            get
            {
                return this._parentidname;
            }
            set
            {
                this._parentidname = value;
                SetAttributeValue("parentidname", value);
            }
        }

        /// <summary>
        /// 路由地址
        /// </summary>
        private string _router;
        [DataMember]
        [Attr("router", "路由地址", AttrType.Varchar, 200)]
        public string router
        {
            get
            {
                return this._router;
            }
            set
            {
                this._router = value;
                SetAttributeValue("Router", value);
            }
        }

        /// <summary>
        /// 菜单索引
        /// </summary>
        private int? _menu_index;
        [DataMember]
        [Attr("menu_index", "菜单索引", AttrType.Int4)]
        public int? menu_Index
        {
            get
            {
                return this._menu_index;
            }
            set
            {
                this._menu_index = value;
                SetAttributeValue("Menu_Index", value);
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        private int? _statecode;
        [DataMember]
        [Attr("statecode", "状态", AttrType.Int4)]
        public int? stateCode
        {
            get
            {
                return this._statecode;
            }
            set
            {
                this._statecode = value;
                SetAttributeValue("stateCode", value);
            }
        }

        private string _statecodename;
        [DataMember]
        [Attr("statecodename", "状态", AttrType.Varchar, 100)]
        public string stateCodeName
        {
            get
            {
                return this._statecodename;
            }
            set
            {
                this._statecodename = value;
                SetAttributeValue("stateCodeName", value);
            }
        }

        /// <summary>
        /// 图标
        /// </summary>
        private string _icon;
        [DataMember]
        [Attr("icon", "图标", AttrType.Varchar, 100)]
        public string icon
        {
            get
            {
                return this._icon;
            }
            set
            {
                this._icon = value;
                SetAttributeValue("icon", value);
            }
        }
    }
}