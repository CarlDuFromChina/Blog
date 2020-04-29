using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Blog
{
    [RequestAuthorize]
    public class BlogController : EntityController<blog, BlogService>
    {
    }
}
