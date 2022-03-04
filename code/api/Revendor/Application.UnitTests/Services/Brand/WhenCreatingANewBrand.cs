using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using Revendor.Domain.Dto;
using Revendor.Domain.Entities;

namespace Application.UnitTests.Services.Brand
{
    [TestFixture]
    public class WhenCreatingANewBrand : ServiceTestBase<BrandService>
    {
        protected override BrandService Service { get; set; }

        protected override void AdditionalSetup()
        {
            Repository.Setup(x => x.Query<TenantBrands>()).Returns(new[]
            {
                new TenantBrands("brand1", "tenant1"),
                new TenantBrands("brand2", "tenant1"),
            }.AsQueryable());
            Service = new BrandService(Repository.Object, Logger.Object);
        }

        [Test]
        public async Task ShouldAddNewBrand()
        {
            var brandDto = new BrandDto();

            var result = await Service.CreateNewBrand(brandDto);

            Assert.True(result.Succeeded);
            Repository.Verify(x => x.AddAsync(It.Is<Revendor.Domain.Entities.Brand>(c =>
                c.IsActive == brandDto.IsActive &&
                c.IsPrivate == brandDto.IsPrivate &&
                c.Image == brandDto.Image &&
                c.Name == brandDto.Name &&
                c.Id == brandDto.Id &&
                c.TenantId == brandDto.TenantId
            )));
            Repository.Verify(x => x.SaveChangesAsync());
        }

        [Test]
        public async Task ShouldAddBrandsToATenant()
        {
            var brands = new TenantBrandDto { TenantId = "1", Brands = new[] { "1", "2", "3" } };

            var result = await Service.UpdateTenantBrands(brands);

            Assert.True(result.Succeeded);
            Repository.Verify(x => x.AddAsync(It.Is<TenantBrands>(c =>
                c.TenantId == brands.TenantId &&
                c.BrandId == brands.Brands[0]
            )));
            Repository.Verify(x => x.AddAsync(It.Is<TenantBrands>(c =>
                c.TenantId == brands.TenantId &&
                c.BrandId == brands.Brands[1]
            )));
            Repository.Verify(x => x.AddAsync(It.Is<TenantBrands>(c =>
                c.TenantId == brands.TenantId &&
                c.BrandId == brands.Brands[2]
            )));
            Repository.Verify(x => x.SaveChangesAsync());
        }

        [Test]
        public async Task ShouldRemoveAllTenantBrandsBeforeAddBrandsToATenant()
        {
            var brands = new TenantBrandDto { TenantId = "tenant1", Brands = new[] { "1" } };

            var result = await Service.UpdateTenantBrands(brands);

            Assert.True(result.Succeeded);
            Repository.Verify(x => x.Remove(It.Is<TenantBrands>(c =>
                c.TenantId == brands.TenantId &&
                c.BrandId == "brand1"
            )));
            Repository.Verify(x => x.Remove(It.Is<TenantBrands>(c =>
                c.TenantId == brands.TenantId &&
                c.BrandId == "brand2"
            )));
        }

        [Test]
        public async Task ShouldReturnErrorWhenSomethingFail()
        {
            Repository.Setup(x => x.SaveChangesAsync()).Throws<Exception>();
            var brandDto = new BrandDto();

            var result = await Service.CreateNewBrand(brandDto);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Erro ao criar uma nova marca. Por favor, contate o suporte."));
        }
    }
}