using FieldAgent.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Agency> Agency { get; set; }
        public DbSet<Agent> Agent { get; set; }
        public DbSet<AgencyAgent> AgencyAgent { get; set; }
        public DbSet<Alias> Alias { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Mission> Mission { get; set; }
        public DbSet<SecurityClearance> SecurityClearances { get; set; }

        public ApplicationDbContext() : base()
        {

        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AgencyAgent>()
                .HasKey(aa => new { aa.AgencyId, aa.AgentId });
        }        
    }
}