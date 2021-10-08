using Blog.Core.Auth;
using Blog.Core.Auth.Privilege;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Entity;
using Sixpence.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.SysMenu
{
    public class SysMenuService : EntityService<sys_menu>
    {
        #region 构造函数
        public SysMenuService()
        {
            this._context = new EntityContext<sys_menu>();
        }

        public SysMenuService(IPersistBroker broker)
        {
            this._context = new EntityContext<sys_menu>(broker);
        }
        #endregion

        public override IList<sys_menu> GetDataList(IList<SearchCondition> searchList, string orderBy, string viewId = "", string searchValue = "")
        {
            var data = base.GetDataList(searchList, orderBy, viewId).Filter().ToList();
            var firstMenu = data.Where(e => string.IsNullOrEmpty(e.parentid)).ToList();
            firstMenu.ForEach(item =>
            {
                item.children = new List<sys_menu>();
                data.ForEach(item2 =>
                {
                    if (item2.parentid == item.Id)
                    {
                        item.children.Add(item2);
                    }
                });
                item.children = item.children.OrderBy(e => e.menu_Index).ToList();
            });
            firstMenu = firstMenu.OrderBy(e => e.menu_Index).ToList();
            return firstMenu;
        }

        public override DataModel<sys_menu> GetDataList(IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            var model = base.GetDataList(searchList, orderBy, pageSize, pageIndex, viewId);
            var data = model.DataList.Filter().ToList();
            var firstMenu = data.Where(e => string.IsNullOrEmpty(e.parentid)).ToList();
            firstMenu.ForEach(item =>
            {
                item.children = new List<sys_menu>();
                data.ForEach(item2 =>
                {
                    if (item2.parentid == item.Id)
                    {
                        item.children.Add(item2);
                    }
                });
                item.children = item.children.OrderBy(e => e.menu_Index).ToList();
            });
            firstMenu = firstMenu.OrderBy(e => e.menu_Index).ToList();
            return new DataModel<sys_menu>() { 
                DataList = firstMenu,
                RecordCount = data.Count()
            };
        }

        public IList<sys_menu> GetFirstMenu()
        {
            var sql = @"
SELECT * FROM sys_menu
WHERE parentid IS NULL
ORDER BY menu_index
";
            var data = Broker.RetrieveMultiple<sys_menu>(sql);
            return data;
        }
    }
}