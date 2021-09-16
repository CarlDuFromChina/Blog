using Blog.Core.Auth;
using Blog.Core.Auth.Privilege;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Module.SysEntity;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sixpence.EntityFramework.Entity
{
    /// <summary>
    /// 实体服务类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityService<T>
        where T : BaseEntity, new()
    {
        /// <summary>
        /// 实体操作
        /// </summary>
        protected EntityContext<T> _context;

        /// <summary>
        /// 数据库持久化
        /// </summary>
        protected IPersistBroker Broker => _context.Broker;

        #region 实体表单

        /// <summary>
        /// 获取视图
        /// </summary>
        /// <returns></returns>
        public virtual IList<EntityView> GetViewList()
        {
            var sql = $"SELECT * FROM {new T().EntityName} WHERE 1=1";
            return new List<EntityView>()
            {
                new EntityView()
                {
                    Sql = sql,
                    CustomFilter = new List<string>() { "name" }, // name 是每个实体必须要添加字段
                    OrderBy = "createdon DESC",
                    ViewId = ""
                }
            };
        }

        /// <summary>
        /// 获取所有记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllData()
        {
            return _context.GetAllEntity();
        }

        /// <summary>
        /// 获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public virtual IList<T> GetDataList(IList<SearchCondition> searchList, string orderBy, string viewId = "", string searchValue = "")
        {
            var view = string.IsNullOrEmpty(viewId) ? GetViewList().ToList().FirstOrDefault() : GetViewList().ToList().Find(item => item.ViewId == viewId);
            return _context.GetDataList(view, searchList, orderBy);
        }

        /// <summary>
        /// 获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public virtual DataModel<T> GetDataList(IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            var view = string.IsNullOrEmpty(viewId) ? GetViewList().ToList().FirstOrDefault() : GetViewList().ToList().Find(item => item.ViewId == viewId);
            var data = _context.GetDataList(view, searchList, orderBy, pageSize, pageIndex, out var recordCount, searchValue);
            return new DataModel<T>()
            {
                DataList = data,
                RecordCount = recordCount
            };
        }

        /// <summary>
        /// 获取实体记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetData(string id)
        {
            var obj = _context.FilteredSingleQuery(id);
            return obj;
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual string CreateData(T t)
        {
            return _context.FilteredCreate(t);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="t"></param>
        public virtual void UpdateData(T t)
        {
            _context.FilteredUpdate(t);
        }

        /// <summary>
        /// 创建或更新记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual string CreateOrUpdateData(T t)
        {
            return _context.FilteredCreateOrUpdateData(t);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids"></param>
        public virtual void DeleteData(List<string> ids)
        {
            _context.FilteredDelete(ids);
        }
        #endregion

        /// <summary>
        /// 获取用户对实体的权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EntityPrivilegeResponse GetPrivilege()
        {
            var sql = @"
SELECT * FROM sys_role_privilege
WHERE sys_roleid = @id and object_type = 'sys_entity'
and objectid = @entityid";
            var user = Broker.Retrieve<user_info>(UserIdentityUtil.GetCurrentUserId());
            var paramList = new Dictionary<string, object>() { { "@id", user.roleid }, { "@entityid", EntityCache.GetEntity(new T().EntityName)?.Id } };
            var data = Broker.Retrieve<sys_role_privilege>(sql, paramList);

            return new EntityPrivilegeResponse()
            {
                read = data.privilege >= 1,
                create = data.privilege >= 3,
                delete = data.privilege >= 7
            };
        }
    }
}
