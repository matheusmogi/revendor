using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revendor.Domain.Entities;

namespace Revendor.Infrastructure.Mapping
{
    public class TenantBrandsMapping : IEntityTypeConfiguration<TenantBrands>
    {
        public void Configure(EntityTypeBuilder<TenantBrands> builder)
        {
            builder.ToTable(nameof(TenantBrands));
            builder
                .ToTable(nameof(TenantBrands))
                .HasKey(x => x.Id)
                .IsClustered(false);
            
            builder.Property(p => p.ClusterId).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
        }
    }
}