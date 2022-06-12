using Blog.Core.Module.SysAttrs;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Blog.Core.Module.SysEntity
{
    public class SysEntityController : EntityBaseController<sys_entity, SysEntityService>
    {
        /// <summary>
        /// 根据实体 id 查询字段
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("attrs")]
        public IList<sys_attrs> GetEntityAttrs(string id)
        {
            return new SysEntityService().GetEntityAttrs(id);
        }

        /// <summary>
        /// 导出实体类
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns></returns>
        [HttpGet("export/cs")]
        public IActionResult ExportCs(string entityId)
        {
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            var result = new SysEntityService().Export(entityId);
            return File(result.bytes, result.ContentType, result.fileName);
        }
    }
}