using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Config
{
    public class MailConfig : ConfigBase<MailConfig>
    {
        public string Name { get; set; }
        public string SMTP { get; set; }
        public string Password { get; set; }
    }
}
