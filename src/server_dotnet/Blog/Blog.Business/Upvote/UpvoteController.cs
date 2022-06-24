using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Upvote
{
    public class UpvoteController : EntityBaseController<upvote, UpvoteService>
    {
        [HttpGet("is_up")]
        public bool IsUp(string objectid)
        {
            return new UpvoteService().IsUp(objectid);
        }
    }
}
