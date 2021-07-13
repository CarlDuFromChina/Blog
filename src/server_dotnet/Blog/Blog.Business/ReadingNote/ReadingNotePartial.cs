using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadingNote
{
    public partial class reading_note
    {
        [DataMember]
        public string isShowName
        {
            get
            {
                return this.is_show == 0 ? "否" : "是";
            }
        }
    }
}
