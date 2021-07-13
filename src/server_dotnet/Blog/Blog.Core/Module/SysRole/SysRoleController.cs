using Blog.Core.Data;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Module.Role
{
    public class SysRoleController : EntityBaseController<sys_role, SysRoleService>
    {
        public IEnumerable<SelectOption> GetBasicRole()
        {
            return new SysRoleService().GetBasicRole();
        }
    }
}
