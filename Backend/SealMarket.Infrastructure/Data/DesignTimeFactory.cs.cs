using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace SealMarket.Infrastructure.Data
{
    public class DesignTimeDbContextFactory :
        Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            var connectionString = "Server=localhost;Database=sealmarket;Uid=seal_app;Pwd=seal2008";

            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new System.Version(8, 0, 0))
            );

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}