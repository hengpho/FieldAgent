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
        public DBFactory DbFac { get; set; }

        public MissionRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<Mission> Insert(Mission mission)
        {
            throw new NotImplementedException();
        }

        public Response Update(Mission mission)
        {
            throw new NotImplementedException();
        }

        public Response Delete(int missionId)
        {
            throw new NotImplementedException();
        }

        public Response<Mission> Get(int missionId)
        {
            throw new NotImplementedException();
        }

        public Response<List<Mission>> GetByAgency(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Response<List<Mission>> GetByAgent(int agentId)
        {
            throw new NotImplementedException();
        }
    }
}
