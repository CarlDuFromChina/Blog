using SixpenceStudio.BaseSite.SysConfig.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Draft
{
    public class DraftConfig : BaseConfig
    {
        public override string Name => "是否启用博客自动备份";

        public override string Code => "enable_draft";

        public override object DefaultValue => "true";
    }
}
