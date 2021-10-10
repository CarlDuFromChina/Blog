﻿using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Entity;
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
        public ReadingNoteService()
        {
            this._context = new EntityContext<reading_note>();
        }

        public ReadingNoteService(IPersistBroker broker)
        {
            this._context = new EntityContext<reading_note>(broker);
        }
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
                    OrderBy = "createdon",
                    Name = "全部阅读笔记"
                },
                new EntityView()
                {
                    ViewId = "03860DF4-0E9E-4330-80BF-6A1E9AC797A6",
                    Sql = @"    
SELECT
	reading_noteid,
	name,
	book_title,
	content,
	surfaceid,
	surface_url,
	big_surfaceid,
	big_surface_url,
	createdby,
	createdbyname,
	createdon,
	modifiedby modifiedbyname,
	modifiedon 
FROM
	reading_note 
WHERE
	is_show = 0
",
                    CustomFilter = new List<string>(){ "name" },
                    OrderBy = "createdon",
                    Name = "展示的阅读笔记"
                }
            };
        }
    }
}