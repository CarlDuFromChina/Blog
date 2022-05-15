using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Blog.Sync.Juejin
{
    public class JuejinController : BaseApiController
    {
        [HttpGet]
        public List<JuejinCategory> Categories()
        {
            return new JuejinService().QueryCategories();
        }

        [HttpGet]
        public List<JuejinTagInfo> Tags(string key_word)
        {
            return new JuejinService().QueryTags(key_word);
        }
    }
}
