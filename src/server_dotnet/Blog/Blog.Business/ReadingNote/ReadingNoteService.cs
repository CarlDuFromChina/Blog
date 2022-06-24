
using Sixpence.ORM.Entity;
using Sixpence.ORM.EntityManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.ReadingNote
{
    public class ReadingNoteService : EntityService<reading_note>
    {
        #region 构造函数
        public ReadingNoteService() : base() { }

        public ReadingNoteService(IEntityManager manager) : base(manager) { }
        #endregion

        public override IList<EntityView> GetViewList()
        {
            return new List<EntityView>()
            {
                new EntityView()
                {
                    ViewId = "B7E8A50C-47E9-4A4F-879B-55D7BC7FFDB6",
                    Sql = "select * from reading_note",
                    CustomFilter = new List<string>(){ "name" },
                    OrderBy = "created_at",
                    Name = "全部阅读笔记"
                },
                new EntityView()
                {
                    ViewId = "03860DF4-0E9E-4330-80BF-6A1E9AC797A6",
                    Sql = @"    
SELECT
	id,
	name,
	book_title,
	content,
	surfaceid,
	surface_url,
	big_surfaceid,
	big_surface_url,
	created_by,
	created_by_name,
	created_at,
	updated_by updated_by_name,
	updated_at 
FROM
	reading_note 
WHERE
	is_show is false
",
                    CustomFilter = new List<string>(){ "name" },
                    OrderBy = "created_at",
                    Name = "展示的阅读笔记"
                }
            };
        }
    }
}
