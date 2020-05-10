using Newtonsoft.Json;
using SixpenceStudio.Platform.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Blog.Blog
{
    public class Blog2Controller : ApiController
    {
        [HttpGet]
        public DataModel<blog> GetDataList(string searchList, string orderBy, int pageSize, int pageIndex)
        {
            var _searchList = string.IsNullOrEmpty(searchList) ? null : JsonConvert.DeserializeObject<IList<SearchCondition>>(searchList);
            return new BlogService().GetDataList(_searchList, orderBy, pageSize, pageIndex);
        }

        [HttpGet]
        public blog GetData(string id)
        {
            return new BlogService().GetData(id);
        }
    }
}
