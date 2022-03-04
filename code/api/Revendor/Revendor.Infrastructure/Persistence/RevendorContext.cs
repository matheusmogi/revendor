using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;
using Revendor.Domain.ValueObjects;
using Revendor.Infrastructure.Mapping;

namespace Revendor.Infrastructure.Persistence
{
    public class RevendorContext : DbContext
    {
        private readonly string connectionString;

        public RevendorContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CustomerMapping());
            modelBuilder.ApplyConfiguration(new TenantMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new BrandMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());
            modelBuilder.ApplyConfiguration(new InventoryMovementMapping());
            modelBuilder.ApplyConfiguration(new TenantBrandsMapping());
        }
         
    }


    public class RevendorContextFactory : IDesignTimeDbContextFactory<RevendorContext>
    {
        public RevendorContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("app.settings.json", true)
                .AddEnvironmentVariables()
                .Build();

            return new RevendorContext(configuration["DefaultConnection"]);
        }
    }
}