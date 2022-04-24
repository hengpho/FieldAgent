using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace FieldAgent.DAL.Test
{
    public class TestMissionRepository
    {
        MissionRepository db;
        DBFactory dbf;
        Mission mission = new Mission
        {
            MissionId = 1,
            AgencyId = 5,
            CodeName = "Rolfs' Milkweed",
            StartDate = new DateTime(2018,11,15),
            ProjectedEndDate = new DateTime(2020,8,21),
            ActualEndDate = new DateTime(2021,10,14),
            OperationalCost = 97154.55M,
            Notes = "In hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus. Nulla ut erat id mauris vulputate elementum."
        };
        Mission updateMission = new Mission
        {
            MissionId = 1,
            AgencyId = 2,
            CodeName = "Update Rolfs' Milkweed",
            StartDate = new DateTime(2018, 10, 10),
            ProjectedEndDate = new DateTime(2020, 7, 12),
            ActualEndDate = new DateTime(2021, 9, 12),
            OperationalCost = 97634.12M,
            Notes = "I am updating the mission"
        };
        Mission addMission = new Mission
        {
            AgencyId = 8,
            CodeName = "New Rolfs' Milkweed",
            StartDate = new DateTime(2018, 11, 15),
            ProjectedEndDate = new DateTime(2020, 8, 21),
            ActualEndDate = new DateTime(2021, 10, 14),
            OperationalCost = 10101.11M,
            Notes = "I am adding a mission"
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new MissionRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(mission.CodeName, db.Get(1).Data.CodeName);
        }

        [Test]
        public void TestUpdate()
        {
            var update = db.Update(updateMission);
            Assert.IsTrue(update.Success);
            Assert.AreEqual(updateMission.CodeName, db.Get(1).Data.CodeName);
        }

        [Test]
        public void TestAdd()
        {
            var add = db.Insert(addMission);
            Assert.IsTrue(add.Success);
            Assert.AreEqual(addMission.CodeName, db.Get(16).Data.CodeName);
        }

        [Test]
        public void TestGetByAgent()
        {
            var getByAgentId = db.GetByAgent(7);
            Assert.IsTrue(getByAgentId.Success);

            foreach (var item in getByAgentId.Data)
            {
                if (item.MissionId == 12)
                {
                    Assert.AreEqual("Elliott's Sedge", item.CodeName);
                }
            }
        }

        [Test]
        public void TestGetByAgency()
        {
            var getByAgencyId = db.GetByAgency(8);
            Assert.IsTrue(getByAgencyId.Success);


            foreach (var item in getByAgencyId.Data)
            {
                if (item.AgencyId == 8)
                {
                    Assert.AreEqual("Schott's Dalea", item.CodeName);
                }
            }
        }

        [Test]
        public void TestDelete()
        {
            var delete = db.Delete(1);
            Assert.IsFalse(db.Get(1).Success);
        }
    }
}
