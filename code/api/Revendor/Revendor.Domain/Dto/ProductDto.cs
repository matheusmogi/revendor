using System;
using System.Collections.Generic;
using System.Linq;
using Revendor.Domain.Entities;

namespace Revendor.Domain.Dto
{
    public class ProductDto : TenantBaseDto
    {
        public ProductDto()
        {
        }

        public ProductDto(Product product)
        {
            Id = product.Id;
            TenantId = product.TenantId;
            Code = product.Code;
            Name = product.Name;
            EAN = product.Code;
            CostPrice = product.CostPrice;
            ProfitMargin = product.ProfitMargin;
            SalePrice = product.SalePrice;
            CurrentInventory = product.CurrentInventory;
            Description = product.Description;
            ValidUntil = product.ValidUntil == DateTime.MinValue ? null : product.ValidUntil?.ToString("yyyy-MM-dd");
            ShowInStore = product.ShowInStore;
            Brand = new BrandDto(product.Brand);
            Images = product.Images?.Select(x => new ImageDto(x)).ToList();
        }

        public string Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string EAN { get; set; }
        public decimal CostPrice { get; set; }
        public decimal ProfitMargin { get; set; }
        public decimal SalePrice { get; set; }
        public decimal CurrentInventory { get; set; }
        public string Description { get; set; }
        public string ValidUntil { get; set; }
        public bool ShowInStore { get; set; }
        public List<ImageDto> Images { get; set; }
        public BrandDto Brand { get; set; }
    }
}