using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace FieldAgent.DAL.Test
{
    public class TestAgencyRepository
    {

        AgencyRepository db;
        DBFactory dbf;
        Agency agency = new Agency
        {
            AgencyId = 1,
            ShortName = "Doti",
            LongName = "Beatson"
        };
        Agency updateAgency = new Agency
        {
            AgencyId = 1,
            ShortName = "UpdatedDoti",
            LongName = "UpdatedBeatson"
        };
        Agency addAgency = new Agency
        {
            ShortName = "NewDoti",
            LongName = "NewBeatson"
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AgencyRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(agency.ShortName, db.Get(1).Data.ShortName);
        }

        [Test]
        public void TestUpdate()
        {
            var update = db.Update(updateAgency);
            Assert.IsTrue(update.Success);
            Assert.AreEqual(updateAgency.ShortName, db.Get(1).Data.ShortName);
        }

        [Test]
        public void TestAdd()
        {
            var add = db.Insert(addAgency);
            Assert.IsTrue(add.Success);
            Assert.AreEqual(addAgency.ShortName, db.Get(11).Data.ShortName);
        }
        
        [Test]
        public void TestGetAll()
        {
            Assert.AreEqual(10, db.GetAll().Data.Count);
        }
    }
}
