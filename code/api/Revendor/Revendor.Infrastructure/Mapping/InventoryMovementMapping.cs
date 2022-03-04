using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revendor.Domain.Entities;

namespace Revendor.Infrastructure.Mapping
{
    public class InventoryMovementMapping : EntityTypeConfigurationBase<InventoryMovement>
    {
        protected override string TableName => nameof(InventoryMovement);

        public override void Configure(EntityTypeBuilder<InventoryMovement> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ProductId).HasMaxLength(50);
            builder.HasOne(a => a.Product)
                .WithMany(a=>a.Movements)
                .HasForeignKey(a=>a.ProductId);
        }
    }
}