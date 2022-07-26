using System.Collections.Generic;
using System.Linq;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sixpence.ORM.Models;

namespace Blog.Comments
{
    public class CommentsController : BaseApiController
    {
        [HttpPost]
        public string CreateData(comments entity)
        {
            return new CommentsService().CreateData(entity);
        }

        [HttpGet("search")]
        public virtual DataModel<comments> GetViewData(string pageSize = "", string pageIndex = "", string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);

            if (string.IsNullOrEmpty(pageSize) || string.IsNullOrEmpty(pageIndex))
            {
                var list = new CommentsService().GetDataList(_searchList, orderBy, viewId, searchValue).ToList();
                return new DataModel<comments>()
                {
                    DataList = list,
                    RecordCount = list.Count
                };
            }

            int.TryParse(pageSize, out var size);
            int.TryParse(pageIndex, out var index);
            return new CommentsService().GetDataList(_searchList, orderBy, size, index, viewId, searchValue);
        }
    }
}
