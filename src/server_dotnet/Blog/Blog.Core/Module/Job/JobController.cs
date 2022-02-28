﻿using Blog.Core.WebApi;
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

        [HttpGet]
        public void RunOnceNow(string name)
        {
            new JobService().RunOnceNow(name);
        }

        [HttpGet]
        public void Pause(string jobName)
        {
            new JobService().Pause(jobName);
        }

        [HttpGet]
        public void Resume(string jobName)
        {
            new JobService().Resume(jobName);
        }
    }
}