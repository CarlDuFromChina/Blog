using Sixpence.Web.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Business.Model;
using Blog.Business.Service;

namespace Blog.Business.Controller
{
    public class AnalysisController : BaseApiController
    {
        [HttpGet("timeline")]
        public IEnumerable<TimelineModel> GetTimeline()
        {
            return new AnalysisService().GetTimeline();
        }
    }
}
