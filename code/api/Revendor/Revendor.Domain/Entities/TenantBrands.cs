using System;
using System.Collections.Generic;

namespace Revendor.Domain.Entities
{
    public class TenantBrands : TenantBasedEntity
    {
        protected TenantBrands()
        {
        }

        public TenantBrands(string brandId, string tenantId) : base(Guid.NewGuid().ToString(), tenantId)
        {
            BrandId = brandId;
        }

        public string BrandId { get; private set; }
        public Brand Brand { get; private set; }
    }
}