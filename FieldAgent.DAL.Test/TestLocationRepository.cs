using FieldAgent.Core.Entities;
using FieldAgent.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace FieldAgent.DAL.Test
{
    public class TestLocationRepository
    {
        LocationRepository db;
        Location location = new Location
        {
            LocationId = 1,
            AgencyId = 9,
            LocationName = "Brazil",
            Street1 = "30655 Pine View Alley",
            Street2 = "56785 Hudson Terrace",
            City = "Barcarena",
            PostalCode = "9",
            CountryCode = "BR"
        };
        Location updateLocation = new Location
        {
            LocationId = 1,
            AgencyId = 5,
            LocationName = "Fake Brazil",
            Street1 = "30655 Update View Alley",
            Street2 = "56785 Update Hudson Terrace",
            City = "FakeBarcarena",
            PostalCode = "971",
            CountryCode = "BR"
        };
        Location addLocation = new Location
        {
            AgencyId = 7,
            LocationName = "New Brazil",
            Street1 = "30655 New Pine View Alley",
            Street2 = "56785 New Hudson Terrace",
            City = "NewBarcarena",
            PostalCode = "911",
            CountryCode = "BR"
        };

        [SetUp]
        public void Setup()
        {
            LocationRepository setup = new LocationRepository(FactoryMode.TEST);
            setup.SetKnownGoodState();
            db = setup;
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(location.Street1, db.Get(1).Data.Street1);
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
            var update = db.Update(updateLocation);
            Assert.IsTrue(update.Success);
            Assert.AreEqual(updateLocation.Street2, db.Get(1).Data.Street2);
        }

        [Test]
        public void TestAdd()
        {
            var add = db.Insert(addLocation);
            Assert.IsTrue(add.Success);
            Assert.AreEqual(addLocation.City, db.Get(11).Data.City);
        }

        [Test]
        public void TestGetByAgency()
        {
            var getByAgentId = db.GetByAgency(4);
            Assert.IsTrue(getByAgentId.Success);
            foreach (var item in getByAgentId.Data)
            {
                if (item.AgencyId == 9)
                {
                    Assert.AreEqual(location.Street1, item.Street1);
                }
            }
        }

    }
}
