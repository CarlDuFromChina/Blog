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
        [Route("data")]
        public IList<job> GetDataList()
        {
            return new JobService().GetDataList();
        }

        [HttpGet]
        [Route("RunOnceNow")]
        public void RunOnceNow(string name)
        {
            new JobService().RunOnceNow(name);
        }

        [HttpGet]
        [Route("Pause")]
        public void Pause(string jobName)
        {
            new JobService().Pause(jobName);
        }

        [HttpGet]
        [Route("Resume")]
        public void Resume(string jobName)
        {
            new JobService().Resume(jobName);
        }
    }
}