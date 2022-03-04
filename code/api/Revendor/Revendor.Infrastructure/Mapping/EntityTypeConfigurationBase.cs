using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revendor.Domain.Entities;

namespace Revendor.Infrastructure.Mapping
{ 
    public abstract class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T> where T : TenantBasedEntity
    {
        protected abstract string TableName { get; }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder
                .ToTable(TableName)
                .HasKey(x => x.Id)
                .IsClustered(false);
            
            builder
                .ToTable(TableName)
                .HasIndex(x => x.ClusterId)
                .IsClustered();

                builder.Property(p => p.ClusterId).ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(x => x.Id).HasMaxLength(50);
            builder.Property(x => x.TenantId).HasMaxLength(50);
            builder.HasOne(x => x.Tenant);
        }
    }
}