using System;
using Sixpence.Web.SysConfig;

namespace Blog.Business.SysConfig
{
	public class WebSiteInfoConfig : ISysConfig
	{
        public string Name => "网站信息";

        public string Code => "website_info";

        public object DefaultValue => "\"{\"\"author\"\": \"\",\"\"email\"\": \"\",\"\"record_no\"\": \"\"\"\"}\"";

        public string Description => "网站信息：作者、邮箱、备案号等";
    }
}

