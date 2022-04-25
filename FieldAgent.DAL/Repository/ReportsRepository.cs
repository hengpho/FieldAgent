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
            response.Data = new List<TopAgentListItem>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("TopAgents", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        response.Data.Add(new TopAgentListItem
                        {
                            NameLastFirst = reader["Name"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            CompletedMissionCount = (int)reader["NumberOfMissions"]
                        });
                        response.Success = true;
                    }
                }
            }

            return response;
        }
        public Response<List<PensionListItem>> GetPensionList(int agencyId)
        {
            Response<List<PensionListItem>> response = new Response<List<PensionListItem>>();
            response.Data = new List<PensionListItem>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("PensionList", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@agencyId", agencyId);
                
                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        response.Data.Add(new PensionListItem
                        {
                            AgencyName = reader["ShortName"].ToString(),
                            BadgeId = (Guid)reader["BadgeId"],
                            NameLastFirst = reader["NameLastFirst"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            DeactivationDate = (DateTime)reader["DeactivationDate"],
                        });
                        response.Success = true;
                    }
                }
            }

            return response;
        }
        
        public Response<List<ClearanceAuditListItem>> AuditClearance(int securityClearanceId, int agencyId)
        {
            Response<List<ClearanceAuditListItem>> response = new Response<List<ClearanceAuditListItem>>();
            response.Data = new List<ClearanceAuditListItem>();
            using (var connection = new SqlConnection(connectionString))
            {
                var cmd = new SqlCommand("ClearanceAudit", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@securityClearanceId", securityClearanceId);
                cmd.Parameters.AddWithValue("@agencyId", agencyId);

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        response.Data.Add(new ClearanceAuditListItem
                        {
                            BadgeId = (Guid)reader["BadgeId"],
                            NameLastFirst = reader["NameLastFirst"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            ActivationDate = (DateTime)reader["ActivationDate"],
                            DeactivationDate = (DateTime)reader["DeactivationDate"],
                        });
                        response.Success = true;
                    }
                }
            }

            return response;
        }
    }
}
