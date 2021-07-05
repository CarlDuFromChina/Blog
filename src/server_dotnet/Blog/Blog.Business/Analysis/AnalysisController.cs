using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Analysis
{
    public class AnalysisController : BaseApiController
    {
        [HttpGet]
        public IEnumerable<TimelineModel> GetTimeline()
        {
            return new AnalysisService().GetTimeline();
        }
    }
}
