using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business
{
    [Route("[controller]")]
    public class IndexController : BaseApiController
    {
        [HttpGet, AllowAnonymous]
        public string Get()
        {
            return "Hello World!"; 
        }
    }
}
