using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;
using Revendor.Domain.ValueObjects;

namespace Revendor.Infrastructure.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable(nameof(User))
                .HasKey(x => x.Id)
                .IsClustered(false);

            builder
                .ToTable(nameof(User))
                .HasIndex(x => x.ClusterId)
                .IsClustered();

            builder.Property(p => p.ClusterId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(x => x.Id).HasMaxLength(50);
            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.Property(x => x.Username).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(200).IsRequired();
            builder.HasOne(x => x.Tenant);

            SeedUser(builder);
        }

        private void SeedUser(EntityTypeBuilder<User> builder)
        {
            builder.HasData(new User(new UserDto
            {
                Id = "655ecf50-191a-4322-8a24-decec9f92116",
                Active = true,
                Password = "$2a$11$ckpeXAuckb7iNaMImlT.B.LMFiMJMIeoPh43rAnugHPObXV9g5b0O",
                Username = "revendor.admin",
                Role = "admin"
            }));


            builder.HasData(new User(new UserDto
            {
                Active = true,
                Id = "4ff9fe1a-4d14-41c4-a03b-cf351ed9f512",
                Password = "$2a$11$ckpeXAuckb7iNaMImlT.B.LMFiMJMIeoPh43rAnugHPObXV9g5b0O",
                Username = "user",
                TenantId = "d93c7c90-e5e5-42b6-b29d-b25c9e55ec8d",
                Role = "tenant"
            }));
        }
    }
}