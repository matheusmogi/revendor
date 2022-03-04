using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revendor.Domain.Entities;

namespace Revendor.Infrastructure.Mapping
{
    public class BrandMapping : EntityTypeConfigurationBase<Brand>
    {
        protected override string TableName => nameof(Brand);

        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.HasMany(x => x.Tenants)
                .WithOne(a => a.Brand);
            builder.HasMany(x => x.Products)
                .WithOne(a => a.Brand);
        }
    }
}