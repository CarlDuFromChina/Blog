﻿using SixpenceStudio.Core;
using SixpenceStudio.Core.Data;
using SixpenceStudio.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixpenceStudio.Blog.idea
{
    public class IdeaSerivice : EntityService<idea>
    {
        #region 构造函数
        public IdeaSerivice()
        {
            _cmd = new EntityCommand<idea>();
        }

        public IdeaSerivice(IPersistBroker broker)
        {
            _cmd = new EntityCommand<idea>(broker);
        }
        #endregion

    }
}