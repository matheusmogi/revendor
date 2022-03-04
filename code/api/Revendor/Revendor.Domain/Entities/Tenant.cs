using System.Collections.Generic;

namespace Revendor.Domain.Entities
{
    public class Tenant
    {
        protected Tenant()
        {
            
        }
        public Tenant(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public string Id { get; private set; }
        public string Name { get; private set; }
        public int ClusterId { get; protected set; }
        public List<TenantBrands> Brands { get; private set; }

    }
}