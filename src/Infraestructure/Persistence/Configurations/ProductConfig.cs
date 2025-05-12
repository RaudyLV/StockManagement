using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Core.Domain.Enums;
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
            
            builder.Property(p => p.Category)
                    .HasConversion<string>()
                    .HasMaxLength(40)
                    .IsRequired();

        }
    }
}