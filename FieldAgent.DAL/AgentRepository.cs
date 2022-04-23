using FieldAgent.Core;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL
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
            throw new NotImplementedException();
        }

        public Response<Agent> Get(int agentId)
        {
            using (var db = DbFac.GetDbContext())
            {
                Response<Agent> response = new Response<Agent>();
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
            throw new NotImplementedException();
        }

        public Response<Agent> Insert(Agent agent)
        {
            throw new NotImplementedException();
        }

        public Response Update(Agent agent)
        {
            throw new NotImplementedException();
        }
    }
}
