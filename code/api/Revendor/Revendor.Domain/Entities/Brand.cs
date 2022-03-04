using System.Collections.Generic;
using System.Threading;
using FluentValidation;
using Revendor.Domain.Dto;

namespace Revendor.Domain.Entities
{
    public class Brand : TenantBasedEntity
    {
        protected Brand()
        {
        }

        public Brand(BrandDto brandDto) : base(brandDto.Id, brandDto.TenantId)
        {
            Image = brandDto.Image;
            Name = brandDto.Name;
            IsActive = brandDto.IsActive;
            IsPrivate = brandDto.IsPrivate;
        }

        public string Image { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsPrivate { get; private set; }
        public List<TenantBrands> Tenants { get; private set; }
        public List<Product> Products { get; private set; }
    }
}