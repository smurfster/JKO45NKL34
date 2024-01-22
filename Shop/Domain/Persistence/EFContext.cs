using Domain.Entities.Customer;
using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence
{
    public class EFContext : DbContext
    {
        public virtual DbSet<CustomerEntity> Customers { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new CustomerEntityConfig());
            modelBuilder.ApplyConfiguration(new ProductEntityConfig());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ShopDB");
        }
    }
}