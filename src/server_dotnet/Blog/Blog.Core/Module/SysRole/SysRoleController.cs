﻿using Blog.Core.Entity;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Blog.Core.Module.Role
{
    public class SysRoleController : EntityBaseController<sys_role, SysRoleService>
    {
        [HttpGet]
        [Route("GetBasicRole")]
        public IEnumerable<SelectOption> GetBasicRole()
        {
            return new SysRoleService().GetBasicRole();
        }

        [HttpGet]
        [Route("GetRoles")]
        public IEnumerable<SelectOption> GetRoles()
        {
            return new SysRoleService().GetRoles();
        }
    }
}
