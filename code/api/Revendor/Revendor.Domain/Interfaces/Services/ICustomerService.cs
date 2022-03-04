using System.Threading.Tasks;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;

namespace Revendor.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Result> CreateNewCustomer(CustomerDto customerDto);
        Task<Result> UpdateCustomer(CustomerDto dto);
        Result GetCustomersByTenantId(string tenant);
        Task<Result> DeleteCustomer(string customerId);
    }
}