using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using SharedTests;

namespace Application.UnitTests.Services.Product
{
    [TestFixture]
    public class WhenCreatingANewProduct : ServiceTestBase<ProductService>
    {
        protected override ProductService Service { get; set; }

        protected override void AdditionalSetup()
        {
            Service = new ProductService(Repository.Object, Logger.Object);
        }

        [Test]
        public async Task ShouldAddNewProduct()
        {
            var productDto = Builders.GivenAProductDto();

            var result = await Service.CreateNewProduct(productDto);

            Assert.True(result.Succeeded);
            Repository.Verify(x => x.AddAsync(It.Is<Revendor.Domain.Entities.Product>(c =>
                c.Id == productDto.Id &&
                c.TenantId == productDto.TenantId &&
                c.Code == productDto.Code &&
                c.Description == productDto.Description &&
                c.Name == productDto.Name &&
                c.EAN == productDto.EAN &&
                c.BrandId==productDto.Brand.Id
            )));
            Repository.Verify(x=>x.SaveChangesAsync());
        }


        [Test]
        public async Task ShouldReturnErrorWhenSomethingFail()
        {
            Repository.Setup(x => x.SaveChangesAsync()).Throws<Exception>();
            var productDto = Builders.GivenAProductDto();

            var result = await Service.CreateNewProduct(productDto);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Erro ao criar um novo produto. Por favor, contate o suporte."));
        }
    }
}