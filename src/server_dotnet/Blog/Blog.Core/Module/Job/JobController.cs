using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Core.Module.Job
{
    [Authorize(Policy = "Api")]
    public class JobController : BaseApiController
    {
        [HttpGet]
        public IList<job> GetDataList()
        {
            return new JobService().GetDataList();
        }

        [HttpPost("run")]
        public void RunOnceNow(string name)
        {
            new JobService().RunOnceNow(name);
        }

        [HttpPost("pause")]
        public void Pause(string name)
        {
            new JobService().Pause(name);
        }

        [HttpPost("resume")]
        public void Resume(string name)
        {
            new JobService().Resume(name);
        }
    }
}