using System;

namespace Revendor.Domain.Dto
{
    public class InventoryMovementDto : TenantBaseDto
    {
        public InventoryMovementDto()
        {
        }
        public InventoryMovementDto(string productId, decimal quantity, string tenantId)
        {
            ProductId = productId;
            Quantity = quantity;
            TenantId = tenantId;
            Id = Guid.NewGuid().ToString();
        }

        public string ProductId { get;  set; }
        public decimal Quantity { get;  set; }
        public string Id { get; set; }
    }
}