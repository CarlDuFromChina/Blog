using SixpenceStudio.BaseSite.SysConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Draft
{
    public class DraftSysConfig : ISysConfig
    {
        public string Name => "是否启用博客自动备份";

        public string Code => "enable_draft";

        public object DefaultValue => "true";
    }
}
