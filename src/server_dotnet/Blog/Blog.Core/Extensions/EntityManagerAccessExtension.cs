using Blog.Core.Auth;
using Blog.Core.Module.SysEntity;
using Sixpence.Common;
using Sixpence.Common.Utils;
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Blog.Core.Extensions
{
    /// <summary>
    /// PersistBroker 权限扩展
    /// </summary>
    public static class EntityManagerAccessExtension
    {
        /// <summary>
        /// 权限创建
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string FilteredCreate(this IEntityManager manager, BaseEntity entity)
        {
            var sysEntity = EntityCache.GetEntity(entity.EntityName);
            if (!AuthAccess.CheckWriteAccess(sysEntity.PrimaryKey.Value))
            {
                throw new InvalidCredentialException($"用户没有实体[{sysEntity.name}]的创建权限");
            }
            return manager.Create(entity);
        }

        /// <summary>
        /// 权限更新
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int FiltededUpdate(this IEntityManager manager, BaseEntity entity)
        {
            var sysEntity = EntityCache.GetEntity(entity.EntityName);
            if (!AuthAccess.CheckWriteAccess(sysEntity.PrimaryKey.Value))
            {
                throw new InvalidCredentialException($"用户没有实体[{sysEntity.name}]的更新权限");
            }
            return manager.Update(entity);
        }

        /// <summary>
        /// 创建或更新历史记录
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string FilteredCreateOrUpdateData(this IEntityManager manager, BaseEntity entity)
        {
            var id = entity.PrimaryKey.Value;
            var isExist = manager.QueryCount($"select count(1) from {entity.EntityName} where {entity.PrimaryKey.Name} = @id", new Dictionary<string, object>() { { "@id", entity.PrimaryKey.Value} }) > 0;
            if (isExist)
            {
                manager.Update(entity);
            }
            else
            {
                id = manager.Create(entity);
            }
            return id;
        }

        /// <summary>
        /// 权限查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T FilteredQueryFirst<T>(this IEntityManager manager, string id) where T : BaseEntity, new()
        {
            var sysEntity = EntityCache.GetEntity(new T().EntityName);
            if (!AuthAccess.CheckReadAccess(sysEntity.PrimaryKey.Value))
            {
                throw new InvalidCredentialException($"用户没有实体[{sysEntity.name}]的查询权限");
            }
            return manager.QueryFirst<T>(id);
        }

        /// <summary>
        /// 权限差选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static IEnumerable<T> FilteredQuery<T>(this IEntityManager manager, string sql, Dictionary<string, object> paramList = null) where T : BaseEntity, new()
        {
            var sysEntity = EntityCache.GetEntity(new T().EntityName);
            if (!AuthAccess.CheckReadAccess(sysEntity.PrimaryKey.Value))
            {
                throw new InvalidCredentialException($"用户没有实体[{sysEntity.name}]的查询权限");
            }
            return manager.Query<T>(sql, paramList);
        }

        /// <summary>
        /// 权限删除
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int FilteredDelete(this IEntityManager manager, BaseEntity entity)
        {
            var sysEntity = EntityCache.GetEntity(entity.EntityName);
            if (!AuthAccess.CheckDeleteAccess(sysEntity.PrimaryKey.Value))
            {
                throw new InvalidCredentialException($"用户没有实体[{sysEntity.name}]的删除权限");
            }
            return manager.Delete(entity);
        }

        /// <summary>
        /// 权限删除
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int FilteredDelete(this IEntityManager manager, string entityName, string id)
        {
            var sysEntity = EntityCache.GetEntity(entityName);
            if (!AuthAccess.CheckDeleteAccess(sysEntity.PrimaryKey.Value))
            {
                throw new InvalidCredentialException($"用户没有实体[{sysEntity.name}]的删除权限");
            }
            return manager.Delete(entityName, id);
        }


        /// <summary>
        /// 删除历史记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        public static void FilteredDelete(this IEntityManager manager, string entityName, IEnumerable<string> ids)
        {
            manager.ExecuteTransaction(() =>
            {
                ids.Each(id =>
                {
                    manager.FilteredDelete(entityName, id);
                });
            });
        }

        /// <summary>
        /// 获取权限条件
        /// </summary>
        /// <param name="broker"></param>
        /// <returns></returns>
        public static string GetFilteredSql(this IEntityManager manager, string ownerName = "created_by")
        {
            AssertUtil.IsNullOrEmpty(UserIdentityUtil.GetCurrentUserId(), "无法获取当前用户");
            return " AND {0}." + ownerName + $" = '{UserIdentityUtil.GetCurrentUserId()}'";
        }
    }
}
