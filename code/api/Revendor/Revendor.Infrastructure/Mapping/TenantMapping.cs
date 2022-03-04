using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;
using Revendor.Domain.Entities;

namespace Revendor.Infrastructure.Mapping
{
    public class TenantMapping : IEntityTypeConfiguration<Tenant>
    {
        private const string TableName = nameof(Tenant);
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder
                .ToTable(TableName)
                .HasKey(x => x.Id)
                .IsClustered(false);
            
            builder
                .ToTable(TableName)
                .HasIndex(x => x.ClusterId)
                .IsClustered();
            
            builder.Property(p => p.ClusterId).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(x => x.Id).HasMaxLength(50);
            builder.Property(x => x.ClusterId).ValueGeneratedOnAdd();
            builder.HasMany(x => x.Brands)
                .WithOne(a => a.Tenant);
            
            builder.HasData(new Tenant("d93c7c90-e5e5-42b6-b29d-b25c9e55ec8d", "Test Tenant"));
        }

    }
}