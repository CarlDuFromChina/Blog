using Sixpence.EntityFramework.Entity;
using Sixpence.Common;
using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixpence.EntityFramework.Entity
{
    public class EntityContext<E> : EntityContextBase<E>
        where E : BaseEntity, new()
    {
        public EntityContext() { }
        public EntityContext(IPersistBroker broker) : base(broker) { }

        /// <summary>
        ///  获取所有实体记录
        /// </summary>
        /// <returns></returns>
        public virtual IList<E> GetAllEntity()
        {
            var sql = $"SELECT *  FROM {new E().EntityName}";
            var data = Broker.RetrieveMultiple<E>(sql);
            return data;
        }

        /// <summary>
        /// 根据搜索条件分页查询
        /// </summary>
        /// <param name="view"></param>
        /// <param name="searchList"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public virtual IList<E> GetDataList(EntityView view, IList<SearchCondition> searchList, string orderBy, int pageSize, int pageIndex, out int recordCount, string searchValue = "")
        {
            var sql = view.Sql;
            var paramList = new Dictionary<string, object>();

            GetSql(ref sql, searchList, ref paramList, orderBy, view, searchValue);

            var recordCountSql = $"SELECT COUNT(1) FROM ({sql}) AS table1";
            recordCount = Convert.ToInt32(Broker.ExecuteScalar(recordCountSql, paramList));
            Broker.DbClient.Driver.AddLimit(ref sql, pageIndex, pageSize);
            var data = Broker.FilteredRetrieveMultiple<E>(sql, paramList);
            return data;
        }

        /// <summary>
        /// 根据搜索条件查询
        /// </summary>
        /// <param name="view">视图</param>
        /// <param name="searchList">搜索条件</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public virtual IList<E> GetDataList(EntityView view, IList<SearchCondition> searchList, string orderBy, string searchValue = "")
        {
            var sql = view.Sql;
            var paramList = new Dictionary<string, object>();

            GetSql(ref sql, searchList, ref paramList, orderBy, view, searchValue);

            var data = Broker.FilteredRetrieveMultiple<E>(sql, paramList);
            return data;
        }

        #region 权限CRUD
        /// <summary>
        /// 创建实体记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual string FilteredCreate(E entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                return "";
            }
            var id = Broker.FilteredCreate(entity);
            return id;
        }

        /// <summary>
        /// 创建或更新历史记录
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual string FilteredCreateOrUpdateData(E entity)
        {
            var id = entity.Id;
            var isExist = SingleQuery(id) != null;
            if (isExist)
            {
                Update(entity);
            }
            else
            {
                id = Create(entity);
            }
            return id;
        }

        /// <summary>
        /// 删除历史记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        public virtual void FilteredDelete(IEnumerable<string> ids)
        {
            Broker.ExecuteTransaction(() =>
            {
                ids.Each(id =>
                {
                    var data = Broker.FilteredRetrieve<E>(id);
                    Broker.FilteredDelete(new E().EntityName, id);
                });
            });
        }


        /// <summary>
        /// 获取实体记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual IEnumerable<E> FilteredQuery(string id)
        {
            return Broker.FilteredRetrieveMultiple<E>(id);
        }

        /// <summary>
        /// 查询单条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual E FilteredSingleQuery(string id)
        {
            return Broker.FilteredRetrieve<E>(id);
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="entity"></param>
        public virtual void FilteredUpdate(E entity)
        {
            if (string.IsNullOrEmpty(entity?.Id))
            {
                return;
            }

            Broker.FiltededUpdate(entity);
        }
        #endregion

        /// <summary>
        /// 格式化Sql
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="searchList"></param>
        /// <param name="paramList"></param>
        /// <param name="orderBy"></param>
        /// <param name="view"></param>
        private void GetSql(ref string sql, IList<SearchCondition> searchList, ref Dictionary<string, object> paramList, string orderBy, EntityView view, string searchValue)
        {
            var entityName = new E().EntityName;
            var count = 0;

            var index = sql.IndexOf("where", StringComparison.CurrentCultureIgnoreCase);
            if (index == -1)
            {
                sql += " WHERE 1=1 ";
            }

            if (!string.IsNullOrEmpty(searchValue) && view.CustomFilter != null)
            {
                foreach (var item in view.CustomFilter)
                {
                    sql += $" AND {entityName}.{item} LIKE @params{count}";
                    paramList.Add($"@params{count++}", $"%{searchValue}%");
                }
            }
            if (searchList != null && searchList.Count() > 0)
            {
                foreach (var search in searchList)
                {
                    var searchCondition = ParseSqlUtil.GetSearchCondition(search.Type, "params", search.Value, ref count);
                    sql += $" AND {entityName}.{search.Name} {searchCondition.sql}";
                    foreach (var item in searchCondition.paramsList)
                    {
                        paramList.Add(item.Key, item.Value);
                    }
                }
            }

            // 以ORDERBY的传入参数优先级最高
            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = string.IsNullOrEmpty(view.OrderBy) ? "" : $" ORDER BY {view.OrderBy}";
            }
            else
            {
                orderBy.Replace("ORDER BY", "", StringComparison.OrdinalIgnoreCase);
                orderBy = $" ORDER BY {orderBy},{new E().EntityName}id";
            }

            sql += orderBy;
        }
    }
}
