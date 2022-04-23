using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FieldAgent.DAL.Test
{
    public class TestAgentRepository
    {
        AgentRepository db;
        DBFactory dbf;
        Agent Vinson = new Agent
        {
            AgentId = 1,
            FirstName = "Vinson",
            LastName = "Leechman",
            DateOfBirth = new DateTime(2007-3-27),
            Height = 5.85M
        };
        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            dbf = new DBFactory(cp.Config, FactoryMode.TEST);
            db = new AgentRepository(dbf);
            dbf.GetDbContext().Database.ExecuteSqlRaw("SetKnownGoodStateAgent");
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(Vinson.FirstName, db.Get(1).Data.FirstName);
        }
    }
}