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
        SecurityClearance securityClearance = new SecurityClearance
        {
            SecurityClearanceId = 1,
            SecurityClearanceName = "None"
        };

        [SetUp]
        public void Setup()
        {
            SecurityClearanceRepository setup = new SecurityClearanceRepository(FactoryMode.TEST);
            setup.SetKnownGoodState();
            db = setup;
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
