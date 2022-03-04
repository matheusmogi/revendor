using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;
using Revendor.Domain.Interfaces;
using Revendor.Domain.Interfaces.Services;

namespace Revendor.Application.Services
{
    public class InventoryMovementService : IInventoryMovementService
    {
        private readonly IRepository repository;
        private readonly ILogger<InventoryMovementService> log;

        public InventoryMovementService(IRepository repository, ILogger<InventoryMovementService> log)
        {
            this.repository = repository;
            this.log = log;
        }

        public async Task<Result> AddNewMovement(InventoryMovementDto dto)
        {
            try
            {
                var result = ChangeInventoryQuantity(dto);
                if (!result)
                {
                    return Result.Failure("Produto não encontrado. Por favor, contate o suporte.");
                }

                var entity = new InventoryMovement(dto);
                await repository.AddAsync(entity);

                await repository.SaveChangesAsync();

                return Result.Success(dto);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return Result.Failure("Erro ao movimentar o estoque. Por favor, contate o suporte.");
            }
        }

        private bool ChangeInventoryQuantity(InventoryMovementDto dto)
        {
            var product = repository.Query<Product>().FirstOrDefault(p => p.Id == dto.ProductId && p.TenantId == dto.TenantId);
            if (product == null)
            {
                return false;
            }
            product.ChangeCurrentInventory(dto.Quantity);
            repository.Update(product);

            return true;
        }
    }
}