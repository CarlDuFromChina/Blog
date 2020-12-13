using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.ReadingNote
{
    public class ReadingNoteService : EntityService<reading_note>
    {
        #region 构造函数
        public ReadingNoteService()
        {
            this._cmd = new EntityCommand<reading_note>();
        }

        public ReadingNoteService(IPersistBroker broker)
        {
            this._cmd = new EntityCommand<reading_note>(broker);
        }
        #endregion
    }
}
