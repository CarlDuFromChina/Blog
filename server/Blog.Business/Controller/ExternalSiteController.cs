using Blog.Business.Config;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Sixpence.Web.Model;
using Sixpence.Web.WebApi;
using System.Collections.Generic;

namespace Blog.Business.Controller
{
    public class ExternalSiteController : BaseApiController
    {
        [HttpGet("sites")]
        public List<SelectOption> GetSites()
        {
            var data = new List<SelectOption>()
            {
                new SelectOption("Extricator", ExternalSiteConfig.Config.Extricator.Host),
                new SelectOption("OnlineTool", ExternalSiteConfig.Config.OnlineTool.Host),
                new SelectOption("Portainer", ExternalSiteConfig.Config.Portainer.Host),
                new SelectOption("WeChatPlatform", ExternalSiteConfig.Config.WeChatPlatform.Host),
            };
            return data;
        }
    }
}
