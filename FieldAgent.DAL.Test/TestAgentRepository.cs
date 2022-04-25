using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace FieldAgent.DAL.Test
{
    public class TestAgentRepository
    {
        AgentRepository db;
        DBFactory dbf;
        Agent agent = new Agent
        {
            AgentId = 1,
            FirstName = "Vinson",
            LastName = "Leechman",
            DateOfBirth = new DateTime(2007,3,27),
            Height = 5.85M
        };
        Agent updateAgent = new Agent
        {
            AgentId = 1,
            FirstName = "UpdatedVinson",
            LastName = "UpdatedLeechman",
            DateOfBirth = new DateTime(2008,3,27),
            Height = 6.85M
        };
        Agent addAgent = new Agent
        {
            FirstName = "NewVinson",
            LastName = "NewLeechman",
            DateOfBirth = new DateTime(2009,3,27),
            Height = 4.85M
        };
        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(agent.FirstName, db.Get(1).Data.FirstName);
        }

        [Test]
        public void TestDelete()
        {
            var delete = db.Delete(1);
            Assert.IsFalse(db.Get(1).Success);
        }
        [Test]
        public void ShouldNoDelete(){
            var delete = db.Delete(11);
            Assert.IsFalse(delete.Success);
        }
        [Test]
        public void TestGetMissionAgent(){

            var missions = db.GetMissions(1);
            Assert.True(missions.Success);
            Assert.AreEqual(6, missions.Data.Count());
            
            foreach (var item in missions.Data)
            {
                if (item.CodeName == "Schott's Dalea")
                {
                    Assert.AreEqual(10, item.MissionId);
                }
            }
        }

        [Test]
        public void TestUpdate()
        {
            var update = db.Update(updateAgent);
            Assert.IsTrue(update.Success);
            Assert.AreEqual(updateAgent.FirstName, db.Get(1).Data.FirstName);
        }

        [Test]
        public void TestAdd()
        {
            var add = db.Insert(addAgent);
            Assert.IsTrue(add.Success);
            Assert.AreEqual(addAgent.LastName, db.Get(11).Data.LastName);
        }
    }
}