using System;
using System.Linq;
using NUnit.Framework;
using Revendor.Application.Services;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using SharedTests;

namespace Application.UnitTests.Services.Product
{
    [TestFixture]
    public class WhenGettingProducts : ServiceTestBase<ProductService>
    {
        private readonly ProductDto productDto = Builders.GivenAProductDto();
        protected override ProductService Service { get; set; }

        protected override void AdditionalSetup()
        {
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Product>())
                .Returns(new[] { new Revendor.Domain.Entities.Product(productDto) }.AsQueryable());
            Service = new ProductService(Repository.Object, Logger.Object);
        }

        [Test]
        public void ShouldGetCustomersFromRepository()
        {
            Service.GetProductsByTenantId(productDto.TenantId);

            Repository.Verify(x => x.Query<Revendor.Domain.Entities.Product>());
        }

        [Test]
        public void ShouldConvertEntityToDtoAndReturn()
        {
            var result = Service.GetProductsByTenantId(productDto.TenantId);

            Assert.IsInstanceOf<Result>(result);
            var product = result.ResponseObject as ProductDto[];
            Assert.That(product?.First().Id, Is.EqualTo(productDto.Id));
            Assert.That(product?.First().Name, Is.EqualTo(productDto.Name));
            Assert.That(product?.First().Brand.Id, Is.EqualTo(productDto.Brand.Id));
            Assert.That(product?.First().Code, Is.EqualTo(productDto.Code));
            Assert.That(product?.First().Description, Is.EqualTo(productDto.Description));
        }
    }
}