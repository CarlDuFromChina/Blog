using Sixpence.ORM.Entity;
using Blog.Core.WebApi;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.ORM.Models;

namespace Blog.ReadingNote
{
    public class ReadingNoteController : EntityBaseController<reading_note, ReadingNoteService>
    {
        [AllowAnonymous]
        public override reading_note GetData(string id)
        {
            return base.GetData(id);
        }

        [AllowAnonymous]
        public override DataModel<reading_note> GetViewData(string searchList, string orderBy, int pageSize, int pageIndex, string viewId = "", string searchValue = "")
        {
            return base.GetViewData(searchList, orderBy, pageSize, pageIndex, viewId, searchValue);
        }
    }
}
