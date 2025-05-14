using Microsoft.EntityFrameworkCore;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infraestructure.Persistence.Configurations
{
    public class BrandConfig : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();
            
            builder.Property(x => x.Description)
                    .HasMaxLength(256)
                    .IsRequired();
            
            builder.Property(x => x.Available)
                    .IsRequired();
            
            builder.HasMany(p => p.Products)
                    .WithOne(p => p.Brand)
                    .HasForeignKey(p => p.BrandId);

                    

        }
    }
}