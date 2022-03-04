using System.Threading.Tasks;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;

namespace Revendor.Domain.Interfaces.Services
{
    public interface IInventoryMovementService
    {
        Task<Result> AddNewMovement(InventoryMovementDto dto);
    }
}