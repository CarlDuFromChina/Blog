using Blog.Core.Data.DbClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Data
{
    public interface IPersistBroker
    {
        IDbClient DbClient { get; }


        #region Create Update Delete

        /// <summary>
        /// 保存实体，系统根据ID自动判断更新还是新建
        /// </summary>
        /// <param name="entity">要保存到数据库的实体对象实例</param>
        /// <returns>穿件或者更新的记录的Id</returns>
        string Save(BaseEntity entity);

        /// <summary>
        /// 创建实体记录
        /// </summary>
        /// <param name="entity">实体对象实例</param>
        /// <returns></returns>
        string Create(BaseEntity entity);

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <param name="entity">实体对象实例</param>
        /// <returns>影响的书库的行数</returns>
        int Update(BaseEntity entity);

        /// <summary>
        /// 删除数据的实体记录
        /// </summary>
        /// <param name="typeName">表的名字</param>
        /// <param name="id">记录的主键Id</param>
        /// <returns>影响的数据库行数</returns>
        int Delete(string typeName, string id);

        /// <summary>
        /// 删除数据库的实体记录
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns>影响的数据库的行数</returns>
        int Delete(BaseEntity obj);

        /// <summary>
        /// 删除实体记录
        /// </summary>
        /// <param name="objArray">实体数组</param>
        /// <returns>影响的记录行数</returns>
        int Delete(BaseEntity[] objArray);

        /// <summary>
        /// 根据条件删除数据库的实体记录
        /// </summary>
        int DeleteByWhere(string typeName, string where, Dictionary<string, object> paramList = null);

        #endregion

        #region Retrieve
        /// <summary>
        /// 根据 实体T 和 实体Id 获取实体对象实例
        /// </summary>
        T Retrieve<T>(string id)
            where T : BaseEntity, new();

        /// <summary>
        /// 根据查询条件查询实体对象
        /// </summary>
        T Retrieve<T>(string sql, Dictionary<string, object> paramList = null)
           where T : BaseEntity, new();
        #endregion

        #region RetrieveMultiple
        /// <summary>
        /// 根据查询条件查询实体的对象列表
        /// </summary>
        IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList = null)
            where T : BaseEntity, new();

        /// <summary>
        /// 根据查询条件查询实体的对象列表 (分页查询）
        /// </summary>
        IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList, string orderby, int pageSize, int pageIndex)
            where T : BaseEntity, new();

        /// <summary>
        /// 根据查询条件查询实体的对象列表 (分页查询）
        /// </summary>
        IList<T> RetrieveMultiple<T>(string sql, Dictionary<string, object> paramList, string orderby, int pageSize, int pageIndex, out int recordCount)
            where T : BaseEntity, new();

        /// <summary>
        /// 根据 id 批量查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        IList<T> RetrieveMultiple<T>(IList<string> ids)
            where T : BaseEntity, new();
        #endregion
    }
}
