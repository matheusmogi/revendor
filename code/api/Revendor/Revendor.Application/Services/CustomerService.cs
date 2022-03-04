using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;
using Revendor.Domain.Interfaces;
using Revendor.Domain.Interfaces.Services;

namespace Revendor.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository repository;
        private readonly ILogger<CustomerService> log;

        public CustomerService(IRepository repository, ILogger<CustomerService> log)
        {
            this.repository = repository;
            this.log = log;
        }

        public async Task<Result> CreateNewCustomer(CustomerDto customerDto)
        {
            try
            {
                var entity = new Customer(customerDto);

                await repository.AddAsync(entity);
                await repository.SaveChangesAsync();

                return Result.Success(customerDto);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return Result.Failure("Erro ao criar um novo cliente. Por favor, contate o suporte.");
            }
        }

        public async Task<Result> UpdateCustomer(CustomerDto dto)
        {
            try
            {
                var entity = repository.Query<Customer>().FirstOrDefault(a => a.Id == dto.Id);
                if (entity == null)
                {
                    return Result.Failure("Cliente não encontrado.");
                }

                entity.Update(dto);
                repository.Update(entity);
                await repository.SaveChangesAsync();

                return Result.Success(dto);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return Result.Failure("Erro ao atualizar o cliente. Por favor, contate o suporte.");
            }
        }

        public Result GetCustomersByTenantId(string tenant)
        {
            var customers = repository.Query<Customer>()
                .Where(a => a.TenantId == tenant)
                .AsEnumerable()
                .Select(a => new CustomerDto(a))
                .ToArray();

            return Result.Success(customers);
        }

        public async Task<Result> DeleteCustomer(string customerId)
        {
            try
            {
                var entity = repository.Query<Customer>().FirstOrDefault(a => a.Id == customerId);
                if (entity == null)
                {
                    return Result.Failure("Cliente não encontrado.");
                }

                await repository.Remove(entity);
                await repository.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return Result.Failure("Erro ao deletar o cliente. Por favor, contate o suporte.");
            }
        }
    }
}