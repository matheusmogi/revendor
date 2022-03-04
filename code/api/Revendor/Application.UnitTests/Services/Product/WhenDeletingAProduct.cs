using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using SharedTests;

namespace Application.UnitTests.Services.Product
{
    [TestFixture]
    public class WhenDeletingAProduct : ServiceTestBase<ProductService>
    {
        private string productId;
        protected override ProductService Service { get; set; }

        protected override void AdditionalSetup()
        {
            Service = new ProductService(Repository.Object, Logger.Object);
            var productDto = Builders.GivenAProductDto();
            productId = productDto.Id;
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Product>())
                .Returns(new[]
                {
                    new Revendor.Domain.Entities.Product(productDto)
                }.AsQueryable());
        }

        [Test]
        public async Task ShouldGetTheUserToBeUpdated()
        {
            await Service.DeleteProduct(productId);

            Repository.Verify(x => x.Query<Revendor.Domain.Entities.Product>());
        }

        [Test]
        public async Task ShouldDeleteTheUser()
        {
            await Service.DeleteProduct(productId);

            Repository.Verify(x => x.Remove(It.Is<Revendor.Domain.Entities.Product>(c => c.Id == productId)));
            Repository.Verify(x=>x.SaveChangesAsync());
        }

        [Test]
        public async Task ShouldReturnErrorWhenSomethingFail()
        {
            Repository.Setup(x => x.SaveChangesAsync()).Throws<Exception>();

            var result = await Service.DeleteProduct(productId);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Erro ao deletar o produto. Por favor, contate o suporte."));
        }

        [Test]
        public async Task ShouldReturnErrorWhenCustomerDoesNotExist()
        {
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Product>()).Returns(Array.Empty<Revendor.Domain.Entities.Product>().AsQueryable);

            var result = await Service.DeleteProduct(productId);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Produto não encontrado."));
        }
    }
}