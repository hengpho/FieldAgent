using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FieldAgent.DAL
{
    public enum FactoryMode
    {
        TEST,
        PROD
    }
    public class DBFactory
    {
        public static DbContextOptions GetDbContext(FactoryMode mode)
        {
            string environment = mode == FactoryMode.TEST ? "Test" : "Prod";

            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<ConfigProvider>();
            var config = builder.Build();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(config[$"ConnectionStrings:{environment}"])
                .Options;
            return options;
        }
    }
}
