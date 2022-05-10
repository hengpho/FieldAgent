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
        DateTime dateOfBirth = new DateTime(2006, 8, 3);
        DateTime DeactivationDate = new DateTime(2021, 1, 21);
        
        DateTime ActivationDate = new DateTime(2020, 3, 18);
        DateTime dateOfBirth2 = new DateTime(2020, 11, 12);
        DateTime DeactivationDate2 = new DateTime(2018, 11, 6);
        [SetUp]
        public void Setup()
        {
            ConfigProvider cp = new ConfigProvider();
            db = new ReportsRepository(cp.Config);
        }

        [Test]
        public void TestGetTopAgents()
        {
            var result = db.GetTopAgents();
            Assert.AreEqual("Vinson Leechman", result.Data[0].NameLastFirst);
            Assert.AreEqual("Edyth Mor", result.Data[1].NameLastFirst);
            Assert.AreEqual("Kitti Stood", result.Data[2].NameLastFirst);

        }

        [Test]
        public void TestGetPensionList() // @agencyId - 10, 7
        {
            var result = db.GetPensionList(10);
            Assert.AreEqual("Maxine", result.Data[0].AgencyName);
            Assert.AreEqual("Mor Edyth", result.Data[0].NameLastFirst);
            Assert.AreEqual(dateOfBirth, result.Data[0].DateOfBirth);
            Assert.AreEqual(DeactivationDate, result.Data[0].DeactivationDate);
        }

        [Test]
        public void TestAuditClearance() // sc = 2   // ag id = 7 
        {
            var result = db.AuditClearance(2, 7);
            Assert.AreEqual("Gribble Kirby", result.Data[0].NameLastFirst);
            Assert.AreEqual(dateOfBirth2, result.Data[0].DateOfBirth);
            Assert.AreEqual(ActivationDate, result.Data[0].ActivationDate);
            Assert.AreEqual(DeactivationDate2, result.Data[0].DeactivationDate);            
        }
    }
}
