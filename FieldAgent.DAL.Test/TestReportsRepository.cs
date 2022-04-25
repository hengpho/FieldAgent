using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace FieldAgent.DAL.Test
{
    public class TestReportsRepository
    {
        ReportsRepository db;
        DBFactory dbf;

        [SetUp]
        public void Setup()
        {
            /*ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new ReportsRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");*/
        }

        [Test]
        public void TestGetTopAgents()
        {

        }

        [Test]
        public void TestGetPensionList()
        {

        }

        [Test]
        public void TestAuditClearance()
        {

        }
    }
}
