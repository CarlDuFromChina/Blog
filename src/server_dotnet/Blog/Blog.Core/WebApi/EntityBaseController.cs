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
using Sixpence.Common.Utils;
using System.IO;

namespace Blog.Core.WebApi
{
    [Authorize(Policy = "Api")]
    public class EntityBaseController<E, S> : BaseApiController
        where E : BaseEntity, new()
        where S : EntityService<E>, new()
    {
        /// <summary>
        /// 获取视图
        /// </summary>
        /// <returns></returns>
        [HttpGet("views")]
        public IList<EntityView> GetViewList()
        {
            return new S().GetViewList();
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
        [HttpGet("data")]
        public virtual DataModel<E> GetViewData(string pageSize = "", string pageIndex = "", string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);

            if (string.IsNullOrEmpty(pageSize) || string.IsNullOrEmpty(pageIndex))
            {
                var list = new S().GetDataList(_searchList, orderBy, viewId, searchValue).ToList();
                return new DataModel<E>()
                {
                    DataList = list,
                    RecordCount = list.Count
                };
            }

            int.TryParse(pageSize, out var size);
            int.TryParse(pageIndex, out var index);
            return new S().GetDataList(_searchList, orderBy, size, index, viewId, searchValue);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
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
        [HttpPut]
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
        [Route("save")]
        public string CreateOrUpdateData(E entity)
        {
            return new S().CreateOrUpdateData(entity);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="ids"></param>
        [HttpDelete("{id}")]
        public void DeleteData(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var ids = id.Split(",").ToList();
                new S().DeleteData(ids);
            }
        }

        [HttpGet]
        [Route("privilege")]
        public EntityPrivilegeResponse GetPrivilege()
        {
            return new S().GetPrivilege();
        }

        [HttpGet, Route("export/csv")]
        public virtual IActionResult ExportCsv()
        {
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            var fileName = new S().Export();
            return File(FileUtil.GetFileStream(fileName), "application/octet-stream", Path.GetFileName(fileName));
        }
    }
}
