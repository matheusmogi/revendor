using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using Revendor.Domain.Dto;
using SharedTests;

namespace Application.UnitTests.Services.Product
{
    [TestFixture]
    public class WhenUpdatingAProduct : ServiceTestBase<ProductService>
    {
        private ProductDto productDto;
        protected override ProductService Service { get; set; }

        protected override void AdditionalSetup()
        {
            productDto = Builders.GivenAProductDto();
            
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Product>())
                .Returns(new[]
                {
                    new Revendor.Domain.Entities.Product(productDto)
                }.AsQueryable());
            Service = new ProductService(Repository.Object, Logger.Object);
        }

        [Test]
        public async Task ShouldGetTheUserToBeUpdated()
        { 
            await Service.UpdateProduct(productDto);

            Repository.Verify(x => x.Query<Revendor.Domain.Entities.Product>());
        }
        
        [Test]
        public async Task ShouldUpdateTheProduct()
        { 
            await Service.UpdateProduct(productDto);

            Repository.Verify(x => x.Update(It.Is<Revendor.Domain.Entities.Product>(c =>
                c.Id == productDto.Id &&
                c.Code == productDto.Code &&
                c.Name == productDto.Name &&
                c.Description == productDto.Description &&
                c.BrandId== productDto.Brand.Id &&
                c.TenantId == productDto.TenantId 
                 
            )));
            Repository.Verify(x=>x.SaveChangesAsync());
        }

        [Test]
        public async Task ShouldReturnErrorWhenSomethingFail()
        {
            Repository.Setup(x => x.SaveChangesAsync()).Throws<Exception>();

            var result = await Service.UpdateProduct(productDto);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Erro ao atualizar o produto. Por favor, contate o suporte."));
        }
        
        [Test]
        public async Task ShouldReturnErrorWhenCustomerDoesNotExist()
        {
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Product>()).Returns(Array.Empty<Revendor.Domain.Entities.Product>().AsQueryable);
            var newProductDto = Builders.GivenAProductDto();

            var result = await Service.UpdateProduct(newProductDto);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Produto não encontrado."));
        }
    }
}