using CsvHelper;
using Sixpence.Common.Config;

namespace Blog.Business.Config
{
    public class ExternalSiteConfig : BaseAppConfig<ExternalSiteConfig>
    {
        /// <summary>
        /// 自动化助手
        /// </summary>
        public WebApiConfig Extricator { get; set; }

        /// <summary>
        /// 在线工具
        /// </summary>
        public WebApiConfig OnlineTool { get; set; }

        /// <summary>
        /// Portainer Docker 管理工具
        /// </summary>
        public WebApiConfig Portainer { get; set; }

        /// <summary>
        /// 微信平台
        /// </summary>
        public WebApiConfig WeChatPlatform { get; set; }
    }

    public class WebApiConfig
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public int Timeout { get; set; }
        public string Token { get; set; }
    }
}
