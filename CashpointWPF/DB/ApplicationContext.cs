using CashpointWPF.DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CashpointWPF.DB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;
        public DbSet<Account> Accounts { get; set; } = null!;

        public ApplicationContext()
        {
            base.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Change the connection string for your case
            //with port and password in appsettings.json file
            optionsBuilder.UseNpgsql(App.Configuration
                .GetSection("AppSettings")
                .GetConnectionString("npgsqlString"));
        }
    }
}