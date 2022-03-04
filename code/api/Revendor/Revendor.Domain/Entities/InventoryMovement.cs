using System;
using Revendor.Domain.Dto;

namespace Revendor.Domain.Entities
{
    public class InventoryMovement : TenantBasedEntity
    {
        protected InventoryMovement()
        {
            
        }
        public InventoryMovement(InventoryMovementDto dto) : base(dto.Id, dto.TenantId)
        {
            ProductId = dto.ProductId;
            Quantity = dto.Quantity;
            CreatedAt = DateTime.Now;
        }

        public Product Product { get; private set; }
        public string ProductId { get; private set; }
        public decimal Quantity { get; private set; }
        public DateTime CreatedAt { get; private set; }
    }
}