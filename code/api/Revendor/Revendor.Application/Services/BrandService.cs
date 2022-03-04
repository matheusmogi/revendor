using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;
using Revendor.Domain.Interfaces;

namespace Revendor.Application.Services
{
    public interface IBrandService
    {
        Task<Result> UpdateTenantBrands(TenantBrandDto dto);
        Task<Result> CreateNewBrand(BrandDto brandDto);
    }

    public class BrandService : IBrandService
    {
        private readonly IRepository repository;
        private readonly ILogger<BrandService> logger;

        public BrandService(IRepository repository, ILogger<BrandService> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<Result> CreateNewBrand(BrandDto brandDto)
        {
            try
            {
                var entity = new Brand(brandDto);

                await repository.AddAsync(entity);
                await repository.SaveChangesAsync();

                return Result.Success(brandDto);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return Result.Failure("Erro ao criar uma nova marca. Por favor, contate o suporte.");
            }
        }

        public async Task<Result> UpdateTenantBrands(TenantBrandDto dto)
        {
            try
            {
                await UnbindCurrentBrands(dto.TenantId);

                var tasks = new List<Task>();
                dto.Brands.ToList().ForEach(brand =>
                {
                    var model = new TenantBrands(brand, dto.TenantId);
                    tasks.Add(repository.AddAsync(model));
                });

                await Task.WhenAll(tasks);
                await repository.SaveChangesAsync();

                return Result.Success();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return Result.Failure("Erro ao adicionar uma marca. Por favor, contate o suporte.");
            }
        }

        private async Task UnbindCurrentBrands(string tenantId)
        {
            var tasks = new List<Task>();
            var tenantBrands = repository
                .Query<TenantBrands>()
                .Where(a => a.TenantId == tenantId)
                .AsQueryable();

            foreach (var t in tenantBrands)
            {
                tasks.Add(repository.Remove(t));
            }

            await Task.WhenAll(tasks);
            await repository.SaveChangesAsync();
        }
    }
}