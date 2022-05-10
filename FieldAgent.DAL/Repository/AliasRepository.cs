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
    public class AliasRepository : IAliasRepository
    {
        private DbContextOptions Dbco;
        public AliasRepository(FactoryMode mode = FactoryMode.TEST)
        {
            Dbco = DBFactory.GetDbContext(mode);
        }

        public Response<Alias> Insert(Alias alias)
        {
            Response<Alias> response = new Response<Alias>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.Alias.Add(alias);
                db.SaveChanges();
                
                response.Data = alias;
                response.Success = true;
                response.Message = "Alias Added";
            }
            return response;
        }

        public Response Update(Alias alias)
        {
            Response response = new Response();
            using (var db = new ApplicationDbContext(Dbco))
            {
                db.Alias.Update(alias);
                db.SaveChanges();
                response.Success = true;
                response.Message = "Updated Alias";
            }
            return response;
        }

        public Response Delete(int aliasId)
        {
            Response response = new Response();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var alias = db.Alias.Find(aliasId);
                if(alias != null)
                {
                    db.Alias.Remove(alias);
                    db.SaveChanges();

                    response.Success = true;
                    response.Message = "Alias deleted successfully";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Alias not found";
                }
            }
            return response;
        }

        public Response<Alias> Get(int aliasId)
        {
            Response<Alias> response = new Response<Alias>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var alias = db.Alias.Find(aliasId);
                if (alias != null)
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

        public Response<List<Alias>> GetByAgent(int agentId)
        {
            Response<List<Alias>> response = new Response<List<Alias>>();
            using (var db = new ApplicationDbContext(Dbco))
            {
                var alias = db.Alias
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
