using Newtonsoft.Json;
using SixpenceStudio.Core.Auth;
using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.WebApi;
using System.Collections.Generic;
using System.Web.Http;

namespace SixpenceStudio.Blog.Comments
{
    public class CommentsController : BaseController
    {
        [HttpPost, AllowAnonymous]
        public string CreateData(comments entity)
        {
            UserIdentityUtil.SetCurrentUser(UserIdentityUtil.GetAnonymous());
            return new CommentsService().CreateData(entity);
        }

        [HttpGet, AllowAnonymous]
        public virtual IList<comments> GetDataList(string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new CommentsService().GetDataList(_searchList, orderBy, viewId, searchValue);
        }
    }
}
