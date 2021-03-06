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
    public class AgencyAgentRepository : IAgencyAgentRepository
    {

        private DbContextOptions Dbco;
        public AgencyAgentRepository(FactoryMode mode = FactoryMode.TEST)
        {
            Dbco = DBFactory.GetDbContext(mode);
        }

        public Response<AgencyAgent> Insert(AgencyAgent agencyAgent)
        {
            Response<AgencyAgent> response = new Response<AgencyAgent>();
            
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.AgencyAgent.Add(agencyAgent);
                db.SaveChanges();

                response.Data = agencyAgent;
                response.Success = true;
                response.Message = "Agency and Agent Added";
            }
            return response;
        }

        public Response Update(AgencyAgent agencyAgent)
        {
            Response response = new Response();
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.AgencyAgent.Update(agencyAgent);
                db.SaveChanges();
                
                response.Success = true;
                response.Message = "Updated Agency and Agent";
            }
            return response;
        }

        public Response Delete(int agencyid, int agentid)
        {
            Response response = new Response();
            try
            {
                using (var db = new ApplicationDbContext(Dbco))
                {
                    db.AgencyAgent.Remove(db.AgencyAgent.Find(agencyid, agentid));
                    db.SaveChanges();
                    response.Success = true;
                    response.Message = "Agent deleted successfully";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<AgencyAgent> Get(int agencyid, int agentid)
        {
            Response<AgencyAgent> response = new Response<AgencyAgent>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var agencyAgent = db.AgencyAgent.Find(agencyid, agentid);
                if (agencyAgent != null)
                {
                    response.Data = agencyAgent;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Agency and Agent not found";
                }
                return response;
            }
        }

        public Response<List<AgencyAgent>> GetByAgency(int agencyId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var alias = db.AgencyAgent
                    .Include(a => a.Agency)
                    .Where(a => a.AgencyId == agencyId)
                    .ToList();

                if (alias.Count > 0)
                {
                    response.Data = alias;
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

        public Response<List<AgencyAgent>> GetByAgent(int agentId)
        {
            Response<List<AgencyAgent>> response = new Response<List<AgencyAgent>>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var alias = db.AgencyAgent
                    .Include(a => a.Agent)
                    .Where(a => a.AgentId == agentId)
                    .ToList();

                if (alias.Count > 0)
                {
                    response.Data = alias;
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
        public void SetKnownGoodState()
        {
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.Database.ExecuteSqlRaw("SetKnownGoodState");
            }
        }
    }
}
