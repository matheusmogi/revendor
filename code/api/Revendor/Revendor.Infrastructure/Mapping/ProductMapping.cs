using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revendor.Domain.Entities;

namespace Revendor.Infrastructure.Mapping
{
    public class ProductMapping : EntityTypeConfigurationBase<Product>
    {
        protected override string TableName => nameof(Product);

        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Code).HasMaxLength(50).IsRequired();
            builder.Property(x => x.EAN).HasMaxLength(50).IsRequired();
            builder.Property(x => x.BrandId).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.OwnsMany(x => x.Images).Property(a=>a.Name).HasMaxLength(100);
            builder.OwnsMany(x => x.Images).Property(a=>a.Path).HasMaxLength(500);
        }
    }
}