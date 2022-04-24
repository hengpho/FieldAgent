using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FieldAgent.DAL.Test
{
    public class TestSecurityClearanceRepository
    {
        SecurityClearanceRepository db;
        DBFactory dbf;
        SecurityClearance securityClearance = new SecurityClearance
        {
            SecurityClearanceId = 1,
            SecurityClearanceName = "None"
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new SecurityClearanceRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(securityClearance.SecurityClearanceName, db.Get(1).Data.SecurityClearanceName);
        }

        [Test]
        public void TestGetAll()
        {
            Assert.AreEqual(5, db.GetAll().Data.Count);
        }
    }
}
