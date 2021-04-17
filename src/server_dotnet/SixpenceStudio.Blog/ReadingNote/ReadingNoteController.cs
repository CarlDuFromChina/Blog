using SixpenceStudio.Core.Entity;
using SixpenceStudio.Core.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace SixpenceStudio.Blog.ReadingNote
{
    public class ReadingNoteController : EntityBaseController<reading_note, ReadingNoteService>
    {
        [AllowAnonymous]
        public override reading_note GetData(string id)
        {
            return base.GetData(id);
        }

        [AllowAnonymous]
        public override DataModel<reading_note> GetDataList(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            return base.GetDataList(searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }
    }
}
