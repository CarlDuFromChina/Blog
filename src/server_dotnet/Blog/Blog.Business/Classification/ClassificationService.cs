﻿using Sixpence.EntityFramework.Broker;
using Sixpence.EntityFramework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Classification
{
    public class ClassificationService : EntityService<classification>
    {
        #region 构造函数
        public ClassificationService()
        {
            this._context = new EntityContext<classification>();
        }

        public ClassificationService(IPersistBroker broker)
        {
            this._context = new EntityContext<classification>(broker);
        }
        #endregion
    }
}