using Newtonsoft.Json;
using SixpenceStudio.Blog.Blog.FriendBlog;
using SixpenceStudio.Platform.Data;
using SixpenceStudio.Platform.Job;
using SixpenceStudio.Platform.Logging;
using SixpenceStudio.Platform.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Blog.SyncJobs
{
    public class SyncBlogJob : JobBase
    {
        public override string Name => "同步博客";

        public override string Description => "同步谢振国的博客";

        public override string CronExperssion => "0 0 4 * * ?";

        public override void Run(IPersistBroker broker)
        {
            LogUtils.DebugLog("开始同步谢振国博客");
            try
            {
                var result = HttpUtil.Get("http://122.51.132.149:8081/index?index=0&offset=5000");
                var blogModel = JsonConvert.DeserializeObject<BlogModel>(result);
                if (result != null && blogModel.statuscode == 200)
                {
                    LogUtils.DebugLog("博客请求成功");
                    blogModel.data.ForEach(item =>
                    {
                        var blog = new friend_blog()
                        {
                            name = item.title,
                            content = item.content,
                            createdOn = ConvertLongToDateTime(item.createTime),
                            modifiedOn = ConvertLongToDateTime(item.updateTime),
                            first_picture = item.firstPicture,
                            author = "谢振国",
                            Id = item.id.ToString()
                        };
                        broker.Save(blog);
                    });
                }
                LogUtils.DebugLog("同步谢振国博客成功");
            }
            catch (Exception e)
            {
                LogUtils.ErrorLog("同步博客出现异常", e);
            }
        }

        private DateTime? ConvertLongToDateTime(long tick)
        {
            if (tick == default(long))
            {
                return null;
            }
            var timeZone = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            return timeZone.Add(new TimeSpan(long.Parse(tick + "0000")));
        }

    }

    public class BlogModel
    {
        public int statuscode { get; set; }
        public string message { get; set; }
        public List<BlogDetail> data { get; set; }
    }

    public class BlogDetail
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string firstPicture { get; set; }
        public long createTime { get; set; }
        public long updateTime { get; set; }
    }
}
