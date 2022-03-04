using System.Threading.Tasks;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;

namespace Revendor.Domain.Interfaces.Services
{
    public interface IProductService
    {
        Task<Result> CreateNewProduct(ProductDto dto);
        Task<Result> UpdateProduct(ProductDto dto);
        Task<Result> DeleteProduct(string productId);
        Result GetProductsByTenantId(string tenant);
    }
}