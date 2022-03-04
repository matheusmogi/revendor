using System;
using System.Collections.Generic;
using System.Linq;
using Revendor.Domain.Dto;
using Revendor.Domain.ValueObjects;

namespace Revendor.Domain.Entities
{
    public class Product : TenantBasedEntity
    {
        internal Product()
        {
        }

        public Product(ProductDto productDto) : base(productDto.Id, productDto.TenantId)
        {
            Code = productDto.Code;
            Name = productDto.Name;
            EAN = productDto.EAN;
            CostPrice = productDto.CostPrice;
            ProfitMargin = productDto.ProfitMargin;
            SalePrice = productDto.SalePrice;
            CurrentInventory = productDto.CurrentInventory;
            Description = productDto.Description;
            ValidUntil = string.IsNullOrEmpty(productDto.ValidUntil) ? default : DateTime.Parse(productDto.ValidUntil);
            ShowInStore = productDto.ShowInStore;
            BrandId = productDto.Brand?.Id;
            Brand = new Brand(productDto.Brand);
            Images = productDto.Images?.Select(x => new Image(x)).ToList();
        }


        public string Code { get; private set; }
        public string Name { get; private set; }
        public string EAN { get; private set; }
        public decimal CostPrice { get; private set; }
        public decimal ProfitMargin { get; private set; }
        public decimal SalePrice { get; private set; }
        public decimal CurrentInventory { get; internal set; }
        public string Description { get; private set; }
        public DateTime? ValidUntil { get; private set; }
        public bool ShowInStore { get; private set; }

        public List<Image> Images { get; private set; }
        public List<InventoryMovement> Movements { get; private set; }
        public string BrandId { get; private set; }
        public Brand Brand { get; private set; }

        public void Update(ProductDto productDto)
        {
            Code = productDto.Code;
            Name = productDto.Name;
            EAN = productDto.EAN;
            CostPrice = productDto.CostPrice;
            ProfitMargin = productDto.ProfitMargin;
            SalePrice = productDto.SalePrice;
            CurrentInventory = productDto.CurrentInventory;
            Description = productDto.Description;
            ValidUntil = string.IsNullOrEmpty(productDto.ValidUntil) ? default : DateTime.Parse(productDto.ValidUntil);
            ShowInStore = productDto.ShowInStore;
            BrandId = productDto.Brand?.Id;
            Images = productDto.Images?.Select(x => new Image(x)).ToList();
        }

        public void ChangeCurrentInventory(decimal quantity)
        {
            CurrentInventory += quantity;
        }
    }
}