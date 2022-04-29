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
    public class MissionRepository : IMissionRepository
    {
        private DbContextOptions Dbco;

        public MissionRepository(FactoryMode mode = FactoryMode.TEST)
        {
            Dbco = DBFactory.GetDbContext(mode);
        }

        public Response<Mission> Insert(Mission mission)
        {
            Response<Mission> response = new Response<Mission>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.Mission.Add(mission);
                db.SaveChanges();

                response.Data = mission;
                response.Success = true;
                response.Message = "Mission Added";
            }
            return response;
        }

        public Response Update(Mission mission)
        {
            Response response = new Response();
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.Mission.Update(mission);
                db.SaveChanges();

                response.Success = true;
                response.Message = "Updated Mission";
            }
            return response;
        }

        public Response Delete(int missionId)
        {
            Response response = new Response();
            try
            {
                using (var db = new ApplicationDbContext(Dbco))
                {
                    var missionAgents = db.MissionAgent
                    .Where(ma => ma.MissionId == missionId);
                    if (missionAgents != null)
                    {
                        foreach (var missionAgent in missionAgents)
                        {
                            db.MissionAgent.Remove(missionAgent);
                        }
                        db.Mission.Remove(db.Mission.Find(missionId));
                        db.SaveChanges();
                        response.Success = true;
                        response.Message = "Agent deleted successfully";
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "No Agent found for this mission";
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public Response<Mission> Get(int missionId)
        {
            Response<Mission> response = new Response<Mission>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var alias = db.Mission.Find(missionId);
                if (alias != null)
                {
                    response.Data = alias;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Missions not found";
                }
                return response;
            }
        }

        public Response<List<Mission>> GetByAgency(int agencyId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var agency = db.Mission
                    .Include(a => a.Agency)
                    .Where(a => a.AgencyId == agencyId)
                    .ToList();

                if (agency.Count > 0)
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

        public Response<List<Mission>> GetByAgent(int agentId)
        {
            Response<List<Mission>> response = new Response<List<Mission>>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                try
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
                catch (Exception ex)
                {
                    response.Success = false;
                    response.Message = ex.Message;
                }
            }
            return response;
        }
    }
}
