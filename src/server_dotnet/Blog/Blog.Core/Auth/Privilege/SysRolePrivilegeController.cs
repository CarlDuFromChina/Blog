using Blog.Core.Auth.Role.BasicRole;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth.Privilege
{
    public class SysRolePrivilegeController : EntityBaseController<sys_role_privilege, SysRolePrivilegeService>
    {
        [HttpGet]
        public IEnumerable<sys_role_privilege> GetUserPrivileges(string roleid, RoleType roleType)
        {
            return new SysRolePrivilegeService().GetUserPrivileges(roleid, roleType);
        }

        [HttpPost]
        public void BulkSave([FromBody]string dataList)
        {
            var privileges = string.IsNullOrEmpty(dataList) ? null : JsonConvert.DeserializeObject<List<sys_role_privilege>>(dataList);
            new SysRolePrivilegeService().BulkSave(privileges);
        }
    }
}
