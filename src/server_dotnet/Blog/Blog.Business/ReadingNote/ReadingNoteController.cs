using Sixpence.ORM.Entity;
using Sixpence.Web.WebApi;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sixpence.ORM.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.ReadingNote
{
    public class ReadingNoteController : EntityBaseController<reading_note, ReadingNoteService>
    {
        [HttpGet("{id}"), AllowAnonymous]
        public override reading_note GetData(string id)
        {
            return base.GetData(id);
        }

        [HttpGet("search"), AllowAnonymous]
        public override DataModel<reading_note> GetViewData(string pageSize = "", string pageIndex = "", string searchList = "", string orderBy = "", string viewId = "", string searchValue = "")
        {
            return base.GetViewData(pageSize, pageIndex, searchList, orderBy, viewId, searchValue);
        }
    }
}
