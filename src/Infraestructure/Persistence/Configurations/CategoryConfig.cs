
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
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
                    .WithMany(p => p.Categories)
                    .UsingEntity(t => t.ToTable("ProductsCategories"));

            

        }
    }
}