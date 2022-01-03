using Blog.Core.Auth.Privilege;
using Sixpence.ORM.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sixpence.ORM.Models;

namespace Blog.Core.WebApi
{
    [Authorize( Policy = "Api")]
    public class EntityBaseController<E, S> : BaseApiController
        where E : BaseEntity, new()
        where S : EntityService<E>, new()
    {
        /// <summary>
        /// 获取视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IList<EntityView> GetViewList()
        {
            return new S().GetViewList();
        }

        /// <summary>
        /// 获取筛选数据
        /// </summary>
        /// <param name="searchList"></param>
        /// <param name="orderBy"></param>
        /// <param name="viewId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual IList<E> GetDataList(string searchList = "", string orderBy = "",string viewId = "", string searchValue = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new S().GetDataList(_searchList, orderBy, viewId, searchValue);
        }

        /// <summary>
        /// 分页获取筛选数据
        /// </summary>
        /// <param name="searchList"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="viewId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual DataModel<E> GetViewData(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new S().GetDataList(_searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public virtual E GetData(string id)
        {
            return new S().GetData(id);
        }

        /// <summary>
        /// 创建数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public string CreateData(E entity)
        {
            return new S().CreateData(entity);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        [HttpPost]
        public void UpdateData(E entity)
        {
            new S().UpdateData(entity);
        }

        /// <summary>
        /// 创建或更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public string CreateOrUpdateData(E entity)
        {
            return new S().CreateOrUpdateData(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids"></param>
        [HttpPost]
        public void DeleteData([FromBody]List<string> ids)
        {
            new S().DeleteData(ids);
        }

        [HttpGet]
        public EntityPrivilegeResponse GetPrivilege()
        {
            return new S().GetPrivilege();
        }
    }
}
