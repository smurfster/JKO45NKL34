using Domain.Entities.Customer;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence
{
    public class EFContext : DbContext, IEFContext
    {
        public virtual DbSet<CustomerEntity> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new CustomerEntityConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ShopDB");
        }
    }
}