using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;
using Revendor.Domain.Interfaces;
using Revendor.Domain.Interfaces.Services;

namespace Revendor.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository repository;
        private readonly ILogger<ProductService> log;

        public ProductService(IRepository repository, ILogger<ProductService> log)
        {
            this.repository = repository;
            this.log = log;
        }

        public async Task<Result> CreateNewProduct(ProductDto productDto)
        {
            try
            {
                var entity = new Product(productDto);

                await repository.AddAsync(entity);
                await repository.SaveChangesAsync();

                return Result.Success(productDto);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return Result.Failure("Erro ao criar um novo produto. Por favor, contate o suporte.");
            }
        }

        public async Task<Result> UpdateProduct(ProductDto dto)
        {
            try
            {
                var entity = repository.Query<Product>().FirstOrDefault(a => a.Id == dto.Id);
                if (entity == null)
                {
                    return Result.Failure("Produto não encontrado.");
                }

                entity.Update(dto);
                repository.Update(entity);
                await repository.SaveChangesAsync();

                return Result.Success(dto);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return Result.Failure("Erro ao atualizar o produto. Por favor, contate o suporte.");
            }
        }

        public Result GetProductsByTenantId(string tenant)
        {
            var products = repository.Query<Product>()
                .Include(x => x.Brand)
                .Where(a => a.TenantId == tenant)
                .AsEnumerable()
                .Select(a => new ProductDto(a))
                .ToArray();

            return Result.Success(products);
        }

        public async Task<Result> DeleteProduct(string productId)
        {
            try
            {
                var entity = repository.Query<Product>().FirstOrDefault(a => a.Id == productId);
                if (entity == null)
                {
                    return Result.Failure("Produto não encontrado.");
                }

                await repository.Remove(entity);
                await repository.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return Result.Failure("Erro ao deletar o produto. Por favor, contate o suporte.");
            }
        }
    }
}