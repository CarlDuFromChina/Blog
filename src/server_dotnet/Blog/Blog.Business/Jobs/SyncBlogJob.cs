using Newtonsoft.Json;
using Quartz;
using Blog.FriendBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Core.Job;
using Blog.Core.Data;
using Sixpence.Core.Utils;
using AutoMapper;
using Blog.Core;
using Blog.Core.Profiles;
using Sixpence.Core;

namespace Blog.Jobs
{
    public class SyncBlogJob : JobBase
    {
        public override string Name => "同步博客";

        public override string Description => "同步谢振国的博客";

        public override IScheduleBuilder ScheduleBuilder => CronScheduleBuilder.CronSchedule("0 0 4 * * ?");

        public override void Executing(IJobExecutionContext context)
        {
            var broker = PersistBrokerFactory.GetPersistBroker();
            var count = 0;
            Logger.Debug("开始同步谢振国博客");
            try
            {
                broker.ExecuteTransaction(() =>
                {
                    var result = HttpUtil.Get("http://122.51.132.149:8081/index?index=0&offset=5000");
                    var blogModel = JsonConvert.DeserializeObject<BlogModel>(result);
                    if (result != null && blogModel.statuscode == 200)
                    {
                        Logger.Debug($"共发现{blogModel.data.Count}篇博客待同步");
                        var dataList = blogModel.data
                            .Select(item => MapperHelper.Map<friend_blog>(item))
                            .ToList();
                        dataList.Each(item => item.author = "谢振国");
                        count = dataList.Count;
                        broker.BulkCreateOrUpdate(dataList);
                    }
                });
            }
            catch (Exception e)
            {
                Logger.Error("同步博客出现异常", e);
            }
            finally
            {
                Logger.Debug($"同步谢振国博客结束，共同步{count}篇博客");
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
            public string description { get; set; }
        }
    }
}
