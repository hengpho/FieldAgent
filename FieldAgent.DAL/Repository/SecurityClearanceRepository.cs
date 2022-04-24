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
    public class SecurityClearanceRepository : ISecurityClearanceRepository
    {
        public DBFactory DbFac { get; set; }

        public SecurityClearanceRepository(DBFactory dbfac)
        {
            DbFac = dbfac;
        }

        public Response<SecurityClearance> Get(int securityClearanceId)
        {
            Response<SecurityClearance> response = new Response<SecurityClearance>();
            using (var db = DbFac.GetDbContext())
            {
                var securityClearance = db.SecurityClearance.Find(securityClearanceId);
                if (securityClearance != null)
                {
                    response.Data = securityClearance;
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Security Clearance not found";
                }
                return response;
            }
        }

        public Response<List<SecurityClearance>> GetAll()
        {
            Response<List<SecurityClearance>> response = new Response<List<SecurityClearance>>();

            using (var db = DbFac.GetDbContext())
            {
                var securityClearances = db.SecurityClearance.ToList();
                if (securityClearances.Count > 0)
                {
                    response.Data = securityClearances;
                    response.Success = true;
                    response.Message = "Security Clearance found";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Security Clearance not found";
                }
                return response;
            }
        }
    }
}
