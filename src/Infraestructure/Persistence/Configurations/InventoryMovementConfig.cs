
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class InventoryMovementConfig : IEntityTypeConfiguration<InventoryMovements>
    {
        public void Configure(EntityTypeBuilder<InventoryMovements> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Change)
                    .IsRequired();

            builder.Property(x => x.Type)
                    .HasMaxLength(10) //Deberia ser 3 por "IN-OUT", pero 10 por si acaso.
                    .IsRequired();
            builder.Property(x => x.Reason)
                    .HasMaxLength(256)
                    .IsRequired();

            builder.HasOne(p => p.Product)
                    .WithOne()
                    .HasForeignKey<InventoryMovements>(p => p.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}