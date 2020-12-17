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
    }
}
