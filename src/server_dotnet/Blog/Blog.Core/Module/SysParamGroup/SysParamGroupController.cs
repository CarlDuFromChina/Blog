using Blog.Core.Entity;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blog.Core.Module.SysParamGroup
{
    public class SysParamGroupController : EntityBaseController<sys_paramgroup, SysParamGroupService>
    {
        [HttpGet]
        public IEnumerable<SelectOption> GetParams(string code)
        {
            return new SysParamGroupService().GetParams(code);
        }

        [HttpGet]
        public IEnumerable<IEnumerable<SelectOption>> GetParamsList(string code)
        {
            var codeList = new string[] { };
            if (!string.IsNullOrEmpty(code))
            {
                codeList = code.Split(',');
            }
            return new SysParamGroupService().GetParamsList(codeList);
        }

        [HttpGet]
        public IEnumerable<IEnumerable<SelectOption>> GetEntitiyList(string code)
        {
            var codeList = new string[] { };
            if (!string.IsNullOrEmpty(code))
            {
                codeList = code.Split(',');
            }
            return new SysParamGroupService().GetEntitiyList(codeList);
        }

        [HttpGet]
        public IActionResult Export(string id)
        {
            HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");
            var result = new SysParamGroupService().Export(id);
            return File(result.bytes, result.ContentType, result.fileName);
        }
    }
}