using Blog.Core.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.WebApi
{
    [ApiController]
    [WebApiExceptionFilter]
    [Route("api/[controller]/[action]")]
    [EnableCors("CorsPolicy")]
    public class BaseApiController : ControllerBase
    {
    }
}
