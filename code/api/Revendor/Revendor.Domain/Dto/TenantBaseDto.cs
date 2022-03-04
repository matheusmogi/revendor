namespace Revendor.Domain.Dto
{
    public abstract class TenantBaseDto
    {
        public string TenantId { get; set; }
    }
    public class GenericTenantDto : TenantBaseDto
    {
    }
}