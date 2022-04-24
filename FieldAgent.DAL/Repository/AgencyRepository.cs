using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Repository
{
    public class AgencyRepository : IAgencyRepository
    {
        public DBFactory DbFac { get; set; }

        public AgencyRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<Agency> Insert(Agency agency)
        {
            Response<Agency> response = new Response<Agency>();
            using (var db = DbFac.GetDbContext())
            {
                db.Agency.Add(agency);
                db.SaveChanges();

                response.Data = agency;
                response.Success = true;
                response.Message = "Agency Added";
            }
            return response;
        }

        public Response Update(Agency agency)
        {
            Response response = new Response();
            using (var db = DbFac.GetDbContext())
            {
                db.Agency.Update(agency);
                db.SaveChanges();
                response.Success = true;
                response.Message = "Updated Agency";
            }
            return response;
        }

        public Response Delete(int agencyId)
        {
            Response response = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var locations = db.Location
                    .Where(a => a.AgencyId == agencyId);
                    foreach (var l in locations)
                    {
                        db.Location.Remove(l);
                    }
                    var agencyAgents = db.AgencyAgent
                    .Where(aa => aa.AgencyId == agencyId);
                    foreach (var agencyAgent in agencyAgents)
                    {
                        db.AgencyAgent.Remove(agencyAgent);
                    }
                    var missions = db.Mission
                    .Where(ma => ma.AgencyId == agencyId);
                    foreach (var m in missions)
                    {
                        db.Mission.Remove(m);
                    }
                    db.Agency.Remove(db.Agency.Find(agencyId));
                    db.SaveChanges();
                    response.Success = true;
                    response.Message = "Agency deleted successfully";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<Agency> Get(int agencyId)
        {
            Response<Agency> response = new Response<Agency>();
            using (var db = DbFac.GetDbContext())
            {
                var agency = db.Agency.Find(agencyId);
                if (agency != null)
                {
                    response.Data = agency;
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

        public Response<List<Agency>> GetAll()
        {
            Response<List<Agency>> response = new Response<List<Agency>>();

            using (var db = DbFac.GetDbContext())
            {
                var agency = db.Agency.ToList();
                if (agency.Count > 0)
                {
                    response.Data = agency;
                    response.Success = true;
                    response.Message = "Agency found";
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
