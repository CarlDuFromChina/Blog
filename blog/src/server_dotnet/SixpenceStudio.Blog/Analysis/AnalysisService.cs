﻿using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.Analysis
{
    public class AnalysisService : BaseService
    {
        public AnalysisService()
        {
            broker = PersistBrokerFactory.GetPersistBroker();
        }

        public AnalysisService(IPersistBroker broker)
        {
            this.broker = broker;
        }

        public IEnumerable<TimelineModel> GetTimeline()
        {
            var blog = broker.Query<TimelineModel>(@"SELECT createdon, title, createdbyname FROM blog");
            var reading = broker.Query<TimelineModel>(@"SELECT createdon, name AS title, createdbyname FROM reading_note");
            return new List<TimelineModel>()
                .Concat(blog)
                .Concat(reading)
                .OrderByDescending(item => item.createdon);
        }
    }
}