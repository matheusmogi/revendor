namespace Revendor.Domain.Entities
{
    public abstract class TenantBasedEntity
    {
        protected TenantBasedEntity()
        {
        }

        protected TenantBasedEntity(string id, string tenantId)
        {
            Id = id;
            TenantId = tenantId;
        } 
        public string Id { get; internal set; }
        public int ClusterId { get; protected set; }
        public string TenantId { get; internal set; }
        public Tenant Tenant { get; private set; }
    }
}