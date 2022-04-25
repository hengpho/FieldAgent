using FieldAgent.Core;
using FieldAgent.Core.DTO;
using FieldAgent.Core.Entities;
using FieldAgent.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL.Repository
{
    public class ReportsRepository : IReportsRepository
    {
        private readonly IConfigurationRoot Config;
        string connectionString;
        private readonly FactoryMode mode;

        public ReportsRepository(IConfigurationRoot config)
        {
            Config = config;
            string environment = mode == FactoryMode.TEST ? "Test" : "Prod";
            connectionString = Config[$"ConnectionStrings:{environment}"];
        }

        public Response<List<TopAgentListItem>> GetTopAgents()
        {
            Response<List<TopAgentListItem>> response = new Response<List<TopAgentListItem>>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("GetTopAgents", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    // response.Data = reader;
                }
            }

            return response;
        }

        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            throw new NotImplementedException();
        }

        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            throw new NotImplementedException();
        }
    }
}
