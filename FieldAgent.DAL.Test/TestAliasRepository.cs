using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FieldAgent.DAL.Test
{
    public class TestAliasRepository
    {
        AliasRepository db;
        DBFactory dbf;
        Alias alias = new Alias
        {
            AliasId = 1,
            AgentId = 4,
            AliasName = "Stacie Frayn",
            InterpolId = Guid.Parse("246ad6fe-01ef-4082-9762-7d248d485dbe"),
            Persona = "Land iguana"
        };

        Alias UpdateAlias = new Alias
        {
            AliasId = 1,
            AgentId = 5,
            AliasName = "Updated Stacie Frayn",
            InterpolId = Guid.Parse("246ad6fe-01ef-4082-9762-7d248d485dbe"),
            Persona = "Sea iguana"
        };

        Alias AddAlias = new Alias
        {
            AgentId = 5,
            AliasName = "Added Stacie Frayn",
            InterpolId = Guid.Parse("246ad6fe-01ef-4082-9762-7d248d485dbe"),
            Persona = "Sky iguana"
        };        
        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AliasRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodState");
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(alias.AliasName, db.Get(1).Data.AliasName);
        }

        [Test]
        public void TestDelete()
        {
            var delete = db.Delete(1);
            Assert.IsTrue(delete.Success);
        }

        [Test]
        public void TestUpdate()
        {
            var update = db.Update(UpdateAlias);
            Assert.IsTrue(update.Success);
            Assert.AreEqual(UpdateAlias.AliasName, db.Get(1).Data.AliasName);
        }

        [Test]
        public void TestAdd()
        {
            var add = db.Insert(AddAlias);
            Assert.IsTrue(add.Success);
            Assert.AreEqual(AddAlias.AliasName, db.Get(11).Data.AliasName);
        }
        
        [Test]
        public void TestGetByAgent()
        {
            var getByAgentId = db.GetByAgent(4);
            Assert.IsTrue(getByAgentId.Success);
            foreach (var item in getByAgentId.Data)
            {
                if (item.AgentId == 5)
                {
                    Assert.AreEqual(alias.AliasName, item.AliasName);
                }
            }
        }
    }
}