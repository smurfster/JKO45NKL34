using Domain.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Persistence
{
    public class ProductEntityConfig : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(nameof(ProductEntity.Id));
            
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Sku).HasConversion(
                sku => sku.Value,
                value => SKU.Create(value)!)
                .IsRequired()
                .HasMaxLength(15);

            builder.HasIndex(p => p.Sku).IsUnique();
        }
    }
}
