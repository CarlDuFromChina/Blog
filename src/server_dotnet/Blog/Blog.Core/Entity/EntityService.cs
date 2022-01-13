using Blog.Core.Auth;
using Blog.Core.Auth.Privilege;
using Blog.Core.Auth.UserInfo;
using Blog.Core.Extensions;
using Blog.Core.Module.SysEntity;
using Sixpence.ORM.EntityManager;
using Sixpence.ORM.Models;
using Sixpence.ORM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sixpence.ORM.Entity
{
    /// <summary>
    /// 实体服务类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityService<T>
        where T : BaseEntity, new()
    {
        public EntityService()
        {
            Repository = new Repository<T>();
        }

        public EntityService(IEntityManager manager)
        {
            Repository = new Repository<T>(manager);
        }

        /// <summary>
        /// 实体操作
        /// </summary>
        protected IRepository<T> Repository;

        /// <summary>
        /// 数据库持久化
        /// </summary>
        protected IEntityManager Manager => Repository.Manager;

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
                    OrderBy = "created_at DESC",
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
            return Repository.GetAllEntity<T>();
        }

        /// <summary>
        /// 获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetDataList(IList<SearchCondition> searchList, string orderBy, string viewId = "", string searchValue = "")
        {
            var view = string.IsNullOrEmpty(viewId) ? GetViewList().ToList().FirstOrDefault() : GetViewList().ToList().Find(item => item.ViewId == viewId);
            return Repository.GetDataList<T>(view, searchList, orderBy);
        }

        /// <summary>
        /// 获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public virtual DataModel<T> GetDataList(IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            var view = string.IsNullOrEmpty(viewId) ? GetViewList().ToList().FirstOrDefault() : GetViewList().ToList().Find(item => item.ViewId == viewId);
            var data = Repository.GetDataList(view, searchList, orderBy, pageSize, pageIndex, out var recordCount, searchValue);
            return new DataModel<T>()
            {
                DataList = data.ToList(),
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
            var obj = Repository.FilteredQueryFirst(id);
            return obj;
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual string CreateData(T t)
        {
            return Repository.FilteredCreate(t);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="t"></param>
        public virtual void UpdateData(T t)
        {
            Repository.FilteredUpdate(t);
        }

        /// <summary>
        /// 创建或更新记录
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual string CreateOrUpdateData(T t)
        {
            return Repository.FilteredCreateOrUpdateData(t);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="ids"></param>
        public virtual void DeleteData(List<string> ids)
        {
            Repository.FilteredDelete(ids);
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
            var user = Manager.QueryFirst<user_info>(UserIdentityUtil.GetCurrentUserId());
            var paramList = new Dictionary<string, object>() { { "@id", user.roleid }, { "@entityid", EntityCache.GetEntity(new T().EntityName)?.PrimaryKey.Value } };
            var data = Manager.QueryFirst<sys_role_privilege>(sql, paramList);

            return new EntityPrivilegeResponse()
            {
                read = data.privilege >= 1,
                create = data.privilege >= 3,
                delete = data.privilege >= 7
            };
        }
    }
}
