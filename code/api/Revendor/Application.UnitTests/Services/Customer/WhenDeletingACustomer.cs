using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using SharedTests;

namespace Application.UnitTests.Services.Customer
{
    [TestFixture]
    public class WhenDeletingACustomer : ServiceTestBase<CustomerService>
    {
        private string customerId;
        protected override CustomerService Service { get; set; }

        protected override void AdditionalSetup()
        {
            Service = new CustomerService(Repository.Object, Logger.Object);
            var customerDto = Builders.GivenACustomerDto();
            customerId = customerDto.Id;
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Customer>())
                .Returns(new[]
                {
                    new Revendor.Domain.Entities.Customer(customerDto)
                }.AsQueryable());
        }

        [Test]
        public async Task ShouldGetTheUserToBeUpdated()
        {
            await Service.DeleteCustomer(customerId);

            Repository.Verify(x => x.Query<Revendor.Domain.Entities.Customer>());
        }

        [Test]
        public async Task ShouldDeleteTheUser()
        {
            await Service.DeleteCustomer(customerId);

            Repository.Verify(x => x.Remove(It.Is<Revendor.Domain.Entities.Customer>(c => c.Id == customerId)));
            Repository.Verify(x=>x.SaveChangesAsync());
        }

        [Test]
        public async Task ShouldReturnErrorWhenSomethingFail()
        {
            Repository.Setup(x => x.SaveChangesAsync()).Throws<Exception>();

            var result = await Service.DeleteCustomer(customerId);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Erro ao deletar o cliente. Por favor, contate o suporte."));
        }

        [Test]
        public async Task ShouldReturnErrorWhenCustomerDoesNotExist()
        {
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Customer>()).Returns(Array.Empty<Revendor.Domain.Entities.Customer>().AsQueryable);

            var result = await Service.DeleteCustomer(customerId);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Cliente não encontrado."));
        }
    }
}