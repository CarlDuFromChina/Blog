using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Blog.Comments
{
    public class CommentsController : EntityBaseController<comments, CommentsService>
    {
        [AllowAnonymous]
        public override IList<comments> GetDataList(string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            return base.GetDataList(searchList, orderBy, viewId, searchValue);
        }
    }
}
