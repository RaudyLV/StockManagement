
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
                builder.HasKey(p => p.Id);

                builder.Property(p => p.Name)
                        .HasMaxLength(25)
                        .IsRequired();

                builder.Property(p => p.Description)
                        .HasMaxLength(256)
                        .IsRequired();
                builder.Property(p => p.UnitPrice)
                        .HasColumnType("decimal(18,2)")
                        .IsRequired();
                builder.Property(p => p.Quantity)
                        .IsRequired();
                
                builder.HasOne(b => b.Brand)
                        .WithMany(p => p.Products)
                        .HasForeignKey(b => b.BrandId);
                
                builder.HasMany(c => c.Categories)
                        .WithMany(p => p.Products);

        
        }
    }
}