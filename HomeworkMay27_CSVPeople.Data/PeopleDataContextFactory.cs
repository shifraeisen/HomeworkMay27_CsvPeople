using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkMay27_CSVPeople.Data;

public class PeopleDataContextFactory : IDesignTimeDbContextFactory<PeopleDataContext>
{
    public PeopleDataContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
           .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), 
           $"..{Path.DirectorySeparatorChar}HomeworkMay27_CSVPeople.Web"))
           .AddJsonFile("appsettings.json")
           .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

        return new PeopleDataContext(config.GetConnectionString("ConStr"));
    }
}