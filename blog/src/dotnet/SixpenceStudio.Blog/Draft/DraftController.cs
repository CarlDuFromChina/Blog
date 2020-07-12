using SixpenceStudio.BaseSite;
using SixpenceStudio.Platform.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Draft
{
    [RequestAuthorize]
    public class DraftController : EntityBaseController<draft, DraftService>
    {
        /// <summary>
        /// 根据博客id获取草稿
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public draft GetDataByBlogId(string id)
        {
            return new DraftService().GetDataByBlogId(id);
        }

        /// <summary>
        /// 获取博客草稿（新建）
        /// </summary>
        /// <returns></returns>
        public IList<draft> GetDrafts()
        {
            return new DraftService().GetDrafts();
        }
    }
}
