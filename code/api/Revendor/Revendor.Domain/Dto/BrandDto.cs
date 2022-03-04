using Revendor.Domain.Entities;

namespace Revendor.Domain.Dto
{
    public class BrandDto : TenantBaseDto
    {
        public BrandDto()
        {
            
        }
        public BrandDto(Brand brand)
        {
            Id = brand.Id;
            TenantId = brand.TenantId;
            Image = brand.Image;
            Name = brand.Name;
            IsActive = brand.IsActive;
            IsPrivate = brand.IsPrivate;
        }

        public string Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrivate { get; set; }
    }
}