using Newtonsoft.Json;
using Blog.Core.Data;
using System.Collections.Generic;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.Core.Auth;

namespace Blog.Comments
{
    public class CommentsController : BaseApiController
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
