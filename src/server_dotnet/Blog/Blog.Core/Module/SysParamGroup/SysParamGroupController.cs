using Sixpence.EntityFramework.Entity;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sixpence.EntityFramework.SelectOption;

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
    }
}