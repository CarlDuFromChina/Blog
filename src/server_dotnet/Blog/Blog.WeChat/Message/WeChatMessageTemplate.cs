#region 类文件描述
/*********************************************************
Copyright @ Sixpence Studio All rights reserved. 
Author   : Karl Du
Created: 2020/11/9 21:48:10
Description：微信消息模板
********************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WeChat.Message
{
    public static class WeChatMessageTemplate
    {
        public static string Text
        {
            get
            {
                return @"
<xml>
  <ToUserName><![CDATA[{0}]]></ToUserName>
  <FromUserName><![CDATA[{1}]]></FromUserName>
  <CreateTime>{2}</CreateTime>
  <MsgType><![CDATA[text]]></MsgType>
  <Content><![CDATA[{3}]]></Content>
</xml>
";
            }
        }
    }
}
