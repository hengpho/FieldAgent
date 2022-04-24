using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace FieldAgent.DAL.Test
{
    public class TestAgencyAgentRepository
    {
        AgencyAgentRepository db;
        DBFactory dbf;
        AgencyAgent AA = new AgencyAgent
        {
            AgencyId = 4,
            AgentId = 2,
            SecurityClearanceId = 4,
            BadgeId = Guid.Parse("dbb14944-ded9-4eae-8f98-a2ad14ee58ed"),
            ActivationDate = new DateTime(2021,6,15),
            DeactivationDate = new DateTime(2019,1,21),
            IsActive = true
        };
        AgencyAgent updateAA = new AgencyAgent
        {
            AgencyId = 4,
            AgentId = 2,
            SecurityClearanceId = 2,
            BadgeId = Guid.Parse("dbb14944-ded9-4eae-8f98-a2ad14ee58ed"),
            ActivationDate = new DateTime(2021,5,15),
            DeactivationDate = new DateTime(2019,2,21),
            IsActive = false
        };
        AgencyAgent addAA = new AgencyAgent
        {
            AgencyId = 1,
            AgentId = 2,
            SecurityClearanceId = 1,
            BadgeId = Guid.Parse("dbb14944-ded9-4eae-8f98-a2ad14ee58ed"),
            ActivationDate = new DateTime(2021,1,11),
            DeactivationDate = new DateTime(2019,1,22),
            IsActive = true
        };
        AgencyAgent getAA = new AgencyAgent
        {
            AgencyId = 7,
            AgentId = 6,
            SecurityClearanceId = 1,
            BadgeId = Guid.Parse("dbb14944-ded9-4eae-8f98-a2ad14ee58ed"),
            ActivationDate = new DateTime(2021,1,11),
            DeactivationDate = new DateTime(2019,1,22),
            IsActive = true
        };

        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AgencyAgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(AA.SecurityClearanceId, db.Get(4, 2).Data.SecurityClearanceId);
        }

        [Test]
        public void TestUpdate()
        {
            var update = db.Update(updateAA);
            Assert.IsTrue(update.Success);
            Assert.AreEqual(updateAA.SecurityClearanceId, db.Get(4, 2).Data.SecurityClearanceId);
        }

        [Test]
        public void TestAdd()
        {
            var add = db.Insert(addAA);
            Assert.IsTrue(add.Success);
            Assert.AreEqual(addAA.AgencyId, db.Get(1, 2).Data.AgencyId);
        }

        [Test]
        public void TestGetByAgent()
        {  
            var getByAgentId = db.GetByAgent(6);
            Assert.IsTrue(getByAgentId.Success);
                       
            foreach (var item in getByAgentId.Data)
            {
                if (item.AgentId == 6)
                {
                    Assert.AreEqual(getAA.AgencyId, item.AgencyId);
                }
            }
        }

        [Test]
        public void TestGetByAgency()
        {
            var getByAgentId = db.GetByAgency(7);
            Assert.IsTrue(getByAgentId.Success);


            foreach (var item in getByAgentId.Data)
            {
                if (item.AgentId == 6)
                {
                    Assert.AreEqual(getAA.AgentId, item.AgentId);
                }
            }
        }

        [Test]
        public void TestDelete()
        {
            var delete = db.Delete(4, 2);
            Assert.IsFalse(db.Get(4, 2).Success);
        }
    }
}
