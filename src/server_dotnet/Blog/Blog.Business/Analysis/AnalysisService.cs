
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Analysis
{
    public class AnalysisService
    {
        private IEntityManager Manager;

        public AnalysisService()
        {
            Manager = EntityManagerFactory.GetManager();
        }

        public AnalysisService(IEntityManager manager)
        {
            this.Manager = manager;
        }

        public IEnumerable<TimelineModel> GetTimeline()
        {
            var post = Manager.Query<TimelineModel>(@"SELECT created_at, title, created_by_name FROM post");
            var reading = Manager.Query<TimelineModel>(@"SELECT created_at, name AS title, created_by_name FROM reading_note");
            return new List<TimelineModel>()
                .Concat(post)
                .Concat(reading)
                .OrderByDescending(item => item.created_at);
        }
    }
}
