using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EFTest
{
    internal class CookbookContextFactory: IDesignTimeDbContextFactory<CookbookContext>
    {
        public CookbookContext CreateDbContext(string[]? args = null)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<CookbookContext>();
            optionsBuilder
                .UseLoggerFactory(LoggerFactory.Create(builder=> builder.AddConsole()))
                .UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]);
            return new CookbookContext(optionsBuilder.Options);
        }
    }
}

