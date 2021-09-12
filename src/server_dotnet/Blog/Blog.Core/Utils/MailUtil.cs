using Blog.Core.Config;
using Blog.Core.Logging;
using log4net;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Blog.Core.Utils
{
    public static class MailUtil
    {
        private static string Sender = MailConfig.Config.Name;
        private static string SMTP = MailConfig.Config.SMTP;
        private static string Password = MailConfig.Config.Password;
        private static ILog logger = LogFactory.GetLogger("mail");

        public static void SendMail(string reciver, string title, string content)
        {
            try
            {
                MailMessage message = new MailMessage();
                MailAddress fromAddr = new MailAddress(Sender);
                message.From = fromAddr;
                message.To.Add(reciver);
                message.Subject = title;
                message.Body = content;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(SMTP, 25);
                client.Credentials = new NetworkCredential(Sender, Password);
                client.EnableSsl = true;
                client.Send(message);

                logger.Debug($"{Sender}成功发送邮件给{reciver}");
            }
            catch (Exception ex)
            {
                logger.Debug("邮件发送失败");
                logger.Error(ex);
                throw new SpException("邮件发送失败，请联系管理员", "D580E104-629E-4588-A15F-0C8AD3B4AA41");
            }
        }
    }
}
