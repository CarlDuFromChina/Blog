using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Auth.UserInfo
{
    public class UserInfoController : EntityBaseController<user_info, UserInfoService>
    {
        [HttpGet, AllowAnonymous]
        public override user_info GetData(string id)
        {
            return base.GetData(id);
        }
    }
}
