using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private DbContextOptions Dbco;

        public LocationRepository(FactoryMode mode = FactoryMode.TEST)
        {
            Dbco = DBFactory.GetDbContext(mode);
        }

        public Response<Location> Insert(Location location)
        {
            Response<Location> response = new Response<Location>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.Location.Add(location);
                db.SaveChanges();

                response.Data = location;
                response.Success = true;
                response.Message = "Location Added";
            }
            return response;
        }

        public Response Update(Location location)
        {
            Response response = new Response();
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.Location.Update(location);
                db.SaveChanges();
                response.Success = true;
                response.Message = "Updated Location";
            }
            return response;
        }

        public Response Delete(int locationId)
        {
            Response response = new Response();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var location = db.Location.Find(locationId);
                if (location != null)
                {
                    db.Location.Remove(location);
                    db.SaveChanges();

                    response.Success = true;
                    response.Message = "Location deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Location not found";
                }
            }
            return response;
        }

        public Response<Location> Get(int locationId)
        {
            Response<Location> response = new Response<Location>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var location = db.Location.Find(locationId);
                if (location != null)
                {
                    response.Data = location;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Agent not found";
                }
                return response;
            }
        }

        public Response<List<Location>> GetByAgency(int agencyId)
        {
            Response<List<Location>> response = new Response<List<Location>>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var location = db.Location
                    .Include(a => a.Agency)
                    .Where(a => a.AgencyId == agencyId)
                    .ToList();

                if (location.Count > 0)
                {
                    response.Data = location;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Agency not found";
                }
                return response;
            }
        }
    }
}
