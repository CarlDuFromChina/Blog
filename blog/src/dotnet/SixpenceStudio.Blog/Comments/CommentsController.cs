using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Blog.Comments
{
    [AllowAnonymous]
    public class CommentsController : EntityBaseController<comments, CommentsService>
    {
    }
}
