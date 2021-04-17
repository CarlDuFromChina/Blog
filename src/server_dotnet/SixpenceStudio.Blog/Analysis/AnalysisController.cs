using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Blog.Analysis
{
    [RequestAuthorize]
    public class AnalysisController : BaseController
    {
        [HttpGet]
        public IEnumerable<TimelineModel> GetTimeline()
        {
            return new AnalysisService().GetTimeline();
        }
    }
}
