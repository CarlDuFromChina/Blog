﻿using Blog.Core;
using Sixpence.EntityFramework.Entity;
using Sixpence.Core.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Blog.Test
{
  [TestFixture]
  public class UnitTestPersistBroker
  {
    [Test]
    public void TestPersistBroker()
    {
      var broker = PersistBrokerFactory.GetPersistBroker("Host=127.0.0.1;Port=5432;User ID=postgres;Password=123123;Database=postgres;");
      Assert.IsNotNull(broker.DbClient);
    }
  }
}
