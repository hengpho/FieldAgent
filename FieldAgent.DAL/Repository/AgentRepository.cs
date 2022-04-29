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
    public class AgentRepository : IAgentRepository
    {
        private DbContextOptions Dbco;

        public AgentRepository(FactoryMode mode = FactoryMode.TEST)
        {
            Dbco = DBFactory.GetDbContext(mode);
        }

        public Response Delete(int agentId)
        {
            Response response = new Response();
            try
            {
                using (var db = new ApplicationDbContext(Dbco))
                {
                    var aliases = db.Alias
                    .Where(a => a.AgentId == agentId);
                    foreach (var alias in aliases)
                    {
                        db.Alias.Remove(alias);
                    }
                    var agencyAgents = db.AgencyAgent
                    .Where(aa => aa.AgentId == agentId);
                    foreach (var agencyAgent in agencyAgents)
                    {
                        db.AgencyAgent.Remove(agencyAgent);
                    }
                    var missionAgents = db.MissionAgent
                    .Where(ma => ma.AgentId == agentId);
                    foreach (var missionAgent in missionAgents)
                    {
                        db.MissionAgent.Remove(missionAgent);
                    }
                    db.Agent.Remove(db.Agent.Find(agentId));
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

        public Response<Agent> Get(int agentId)
        {
            Response<Agent> response = new Response<Agent>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var agent = db.Agent.Find(agentId);
                if (agent != null)
                {
                    response.Data = agent;
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

        public Response<List<Mission>> GetMissions(int agentId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();
            try
            {
                using (var db = new ApplicationDbContext(Dbco))
                {

                    var mission = db.Mission
                        .Include(m => m.MissionAgent)
                        .ToList();

                    if (mission != null)
                    {
                        response.Data = mission
                            .Where(m => m.MissionAgent
                            .Any(ma => ma.AgentId == agentId))
                            .ToList();
                        response.Success = true;
                        response.Message = "Missions found";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Agent not found";
                    }
                }
            }                
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

            }
            return response;            
        }
            
            

        public Response<Agent> Insert(Agent agent)
        {
            Response<Agent> response = new Response<Agent>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.Agent.Add(agent);
                db.SaveChanges();

                response.Data = agent;
                response.Success = true;
                response.Message = "Agent Added";
            }
            return response;
        }

        public Response Update(Agent agent)
        {
            Response response = new Response();
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.Agent.Update(agent);
                db.SaveChanges();
                response.Success = true;
                response.Message = "Updated Agent";
            }
            return response;
        }
    }
}
