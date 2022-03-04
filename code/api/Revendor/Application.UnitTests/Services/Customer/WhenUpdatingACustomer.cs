using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using Revendor.Domain.Dto;
using SharedTests;

namespace Application.UnitTests.Services.Customer
{
    [TestFixture]
    public class WhenUpdatingACustomer : ServiceTestBase<CustomerService>
    {
        private CustomerDto customerDto;
        protected override CustomerService Service { get; set; }

        protected override void AdditionalSetup()
        {
            customerDto = Builders.GivenACustomerDto();
            
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Customer>())
                .Returns(new[]
                {
                    new Revendor.Domain.Entities.Customer(customerDto)
                }.AsQueryable());
            Service = new CustomerService(Repository.Object, Logger.Object);
        }

        [Test]
        public async Task ShouldGetTheUserToBeUpdated()
        { 
            await Service.UpdateCustomer(customerDto);

            Repository.Verify(x => x.Query<Revendor.Domain.Entities.Customer>());
        }
        
        [Test]
        public async Task ShouldUpdateTheUser()
        { 
            await Service.UpdateCustomer(customerDto);

            Repository.Verify(x => x.Update(It.Is<Revendor.Domain.Entities.Customer>(c =>
                c.Id == customerDto.Id &&
                c.TenantId == customerDto.TenantId &&
                c.Address.City == customerDto.Address.City &&
                c.Address.Complement == customerDto.Address.Complement &&
                c.Address.Neighbourhood == customerDto.Address.Neighbourhood &&
                c.Address.State == customerDto.Address.State &&
                c.Address.AddressLine == customerDto.Address.AddressLine &&
                c.Address.StreetNumber == customerDto.Address.StreetNumber &&
                c.Address.ZipCode == customerDto.Address.ZipCode &&
                c.Birthday ==DateTime.Parse(customerDto.Birthday) &&
                c.Name == customerDto.Name &&
                c.Notes == customerDto.Notes &&
                c.Email == customerDto.Email&&
                c.PhoneNumbers.First().PhoneNumber == customerDto.PhoneNumbers.First().PhoneNumber &&
                c.PhoneNumbers.First().Label == customerDto.PhoneNumbers.First().Label
            )));
            Repository.Verify(x=>x.SaveChangesAsync());
        }

        [Test]
        public async Task ShouldReturnErrorWhenSomethingFail()
        {
            Repository.Setup(x => x.SaveChangesAsync()).Throws<Exception>();

            var result = await Service.UpdateCustomer(customerDto);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Erro ao atualizar o cliente. Por favor, contate o suporte."));
        }
        
        [Test]
        public async Task ShouldReturnErrorWhenCustomerDoesNotExist()
        {
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Customer>()).Returns(Array.Empty<Revendor.Domain.Entities.Customer>().AsQueryable);
            var newCustomerDto = Builders.GivenACustomerDto();

            var result = await Service.UpdateCustomer(newCustomerDto);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Cliente não encontrado."));
        }
    }
}