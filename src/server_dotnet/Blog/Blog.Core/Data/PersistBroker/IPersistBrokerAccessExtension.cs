using Blog.Core.Auth;
using Blog.Core.Module.SysEntity;
using Sixpence.Core;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Blog.Core.Data
{
    /// <summary>
    /// PersistBroker 权限扩展
    /// </summary>
    public static class IPersistBrokerAccessExtension
    {
        /// <summary>
        /// 权限创建
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string FilteredCreate(this IPersistBroker broker, BaseEntity entity)
        {
            var sysEntity = EntityCache.GetEntity(entity.EntityName);
            AssertUtil.CheckBoolean<InvalidCredentialException>(!AuthAccess.CheckWriteAccess(sysEntity.Id), $"用户没有实体[{sysEntity.name}]的创建权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Create(entity);
        }

        /// <summary>
        /// 权限更新
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int FiltededUpdate(this IPersistBroker broker, BaseEntity entity)
        {
            var sysEntity = EntityCache.GetEntity(entity.EntityName);
            AssertUtil.CheckBoolean<InvalidCredentialException>(!AuthAccess.CheckWriteAccess(sysEntity.Id), $"用户没有实体[{sysEntity.name}]的更新权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Update(entity);
        }

        /// <summary>
        /// 权限查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T FilteredRetrieve<T>(this IPersistBroker broker, string id) where T : BaseEntity, new()
        {
            var sysEntity = EntityCache.GetEntity(new T().EntityName);
            AssertUtil.CheckBoolean<InvalidCredentialException>(!AuthAccess.CheckReadAccess(sysEntity.Id), $"用户没有实体[{sysEntity.name}]的查询权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Retrieve<T>(id);
        }

        /// <summary>
        /// 权限差选
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="broker"></param>
        /// <param name="sql"></param>
        /// <param name="paramList"></param>
        /// <returns></returns>
        public static IList<T> FilteredRetrieveMultiple<T>(this IPersistBroker broker, string sql, Dictionary<string, object> paramList = null) where T : BaseEntity, new()
        {
            var sysEntity = EntityCache.GetEntity(new T().EntityName);
            AssertUtil.CheckBoolean<InvalidCredentialException>(!AuthAccess.CheckReadAccess(sysEntity.Id), $"用户没有实体[{sysEntity.name}]的查询权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.RetrieveMultiple<T>(sql, paramList);
        }

        /// <summary>
        /// 权限删除
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static int FilteredDelete(this IPersistBroker broker, BaseEntity entity)
        {
            var sysEntity = EntityCache.GetEntity(entity.EntityName);
            AssertUtil.CheckBoolean<InvalidCredentialException>(!AuthAccess.CheckDeleteAccess(sysEntity.Id), $"用户没有实体[{sysEntity.name}]的删除权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Delete(entity);
        }

        /// <summary>
        /// 权限删除
        /// </summary>
        /// <param name="broker"></param>
        /// <param name="entityName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int FilteredDelete(this IPersistBroker broker, string entityName, string id)
        {
            var sysEntity = EntityCache.GetEntity(entityName);
            AssertUtil.CheckBoolean<InvalidCredentialException>(!AuthAccess.CheckDeleteAccess(sysEntity.Id), $"用户没有实体[{sysEntity.name}]的删除权限", "451FC4BA-46B2-4838-B8D0-69617DFCAF39");
            return broker.Delete(entityName, id);
        }

        /// <summary>
        /// 获取权限条件
        /// </summary>
        /// <param name="broker"></param>
        /// <returns></returns>
        public static string GetFilteredSql(this IPersistBroker broker, string ownerName = "createdBy")
        {
            AssertUtil.CheckIsNullOrEmpty<SpException>(UserIdentityUtil.GetCurrentUserId(), "无法获取当前用户", "11F1C19C-D69E-4A46-BAB6-BAE84E32F7B2");
            return " AND {0}." + ownerName + $" = '{UserIdentityUtil.GetCurrentUserId()}'";
        }
    }
}
