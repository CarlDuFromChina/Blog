using NUnit.Framework;
using Sixpence.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Test
{
    [TestFixture]
    public class HtmlUtilTest
    {
        [Test]
        public void Test()
        {
            var html = @"<img src=""http://karldu.cn/api/SysFile/Download?objectId=09f1f7cb-5b69-44e4-9a30-2eac785520a0"" alt=""image.png"">";
            var list = HtmlUtil.GetHtmlImageUrlList(html);
            Assert.IsTrue(list != null && list.Count > 0);
            Assert.IsTrue(list[0] == "http://karldu.cn/api/SysFile/Download?objectId=09f1f7cb-5b69-44e4-9a30-2eac785520a0");
        }
    }
}
