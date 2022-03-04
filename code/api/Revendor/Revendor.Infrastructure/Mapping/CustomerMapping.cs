using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Revendor.Domain.Entities;

namespace Revendor.Infrastructure.Mapping
{
    public class CustomerMapping : EntityTypeConfigurationBase<Customer>
    {
        protected override string TableName => "Customer";

        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            
            builder.OwnsOne(x => x.Gender).Property(a => a.Name).HasMaxLength(20).HasColumnName("Gender");
            builder.OwnsOne(x => x.Address).Property(a => a.AddressLine).HasMaxLength(100).HasColumnName("AddressLine");
            builder.OwnsOne(x => x.Address).Property(a => a.Complement).HasMaxLength(100).HasColumnName("Complement");
            builder.OwnsOne(x => x.Address).Property(a => a.Neighbourhood).HasMaxLength(100).HasColumnName("Neighbourhood");
            builder.OwnsOne(x => x.Address).Property(a => a.State).HasMaxLength(2).HasColumnName("State");
            builder.OwnsOne(x => x.Address).Property(a => a.StreetNumber).HasMaxLength(10).HasColumnName("StreetNumber");
            builder.OwnsOne(x => x.Address).Property(a => a.ZipCode).HasMaxLength(10).HasColumnName("ZipCode");
            builder.OwnsMany(x => x.PhoneNumbers).Property(a=>a.PhoneNumber).HasMaxLength(15);
            builder.OwnsMany(x => x.PhoneNumbers).Property(a=>a.Label).HasMaxLength(20);
            
            builder.Property(x => x.Birthday).HasMaxLength(10);
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Email).HasMaxLength(100);
            builder.Property(x => x.Tags).HasMaxLength(300);
        }
    }
}