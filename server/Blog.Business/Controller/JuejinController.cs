using Sixpence.Web.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Business.Model;
using Blog.Business.Service;

namespace Blog.Business.Controller
{
    public class JuejinController : BaseApiController
    {
        [HttpGet, Route("categories")]
        public List<JuejinCategory> Categories()
        {
            return new JuejinService().QueryCategories();
        }

        [HttpGet, Route("tags")]
        public List<JuejinTagInfo> Tags(string key_word)
        {
            return new JuejinService().QueryTags(key_word);
        }
    }
}
