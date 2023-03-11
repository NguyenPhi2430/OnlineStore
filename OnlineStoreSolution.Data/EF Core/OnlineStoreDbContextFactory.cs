using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreSolution.Data.EF_Core
{
    public class OnlineStoreDbContextFactory : IDesignTimeDbContextFactory<OnlineStoreDBContext>
    {
        public OnlineStoreDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("OnlineStoreDb");

            var optionsBuilder = new DbContextOptionsBuilder<OnlineStoreDBContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new OnlineStoreDBContext(optionsBuilder.Options);
        }
    }
}
