
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<DomainUser>
    {
        public void Configure(EntityTypeBuilder<DomainUser> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.FullName)
                    .HasMaxLength(256)
                    .IsRequired();
            
        }
    }
}