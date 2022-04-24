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
        public DBFactory DbFac { get; set; }

        public AgentRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response Delete(int agentId)
        {
            Response response = new Response();
            try
            {
                using (var db = DbFac.GetDbContext())
                {
                    var agent = db.AgencyAgent
                        .Include(a => a.Agent)
                            .ThenInclude(m => m.Missions)
                    .Where(a => a.AgentId == agentId)
                    .ToList();

                    if (agent != null)
                    {
                        foreach (var a in agent)
                        {
                            db.AgencyAgent.Remove(a);
                        }
                        db.SaveChanges();
                        response.Success = true;
                        response.Message = "Agent deleted successfully";
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

        public Response<Agent> Get(int agentId)
        {
            Response<Agent> response = new Response<Agent>();
            using (var db = DbFac.GetDbContext())
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
            using (var db = DbFac.GetDbContext())
            {
                try
                {
                    foreach (var a in db.Mission
                        .Include(a => a.Agents)
                        .Where(a => a.Agents
                        .Any(m => m.AgentId == agentId))
                        .ToList())
                    {
                        response.Data.Add(a);
                    }

                    /*var Mission = db.Mission
                        .Include(m => m.AgencyId)
                        .Where(m => m.Agents
                        .Any(a => a.AgentId == agentId))
                        .ToList();*/
                    
                    if (response.Data != null)
                    {
                        response.Success = true;
                        response.Message = "Missions found";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Agent not found";
                    }
                }
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }
            return response;
        }

        public Response<Agent> Insert(Agent agent)
        {
            Response<Agent> response = new Response<Agent>();
            using (var db = DbFac.GetDbContext())
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
            using (var db = DbFac.GetDbContext())
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
