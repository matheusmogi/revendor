using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using SharedTests;

namespace Application.UnitTests.Services.Customer
{
    [TestFixture]
    public class WhenCreatingANewCustomer : ServiceTestBase<CustomerService>
    {
        protected override CustomerService Service { get; set; }

        protected override void AdditionalSetup()
        {
            Service = new CustomerService(Repository.Object, Logger.Object);
        }

        [Test]
        public async Task ShouldAddNewCustomer()
        {
            var customerDto = Builders.GivenACustomerDto();

            var result = await Service.CreateNewCustomer(customerDto);

            Assert.True(result.Succeeded);
            Repository.Verify(x => x.AddAsync(It.Is<Revendor.Domain.Entities.Customer>(c =>
                c.Id == customerDto.Id &&
                c.TenantId == customerDto.TenantId &&
                c.Address.City == customerDto.Address.City &&
                c.Address.Complement == customerDto.Address.Complement &&
                c.Address.Neighbourhood == customerDto.Address.Neighbourhood &&
                c.Address.State == customerDto.Address.State &&
                c.Address.AddressLine == customerDto.Address.AddressLine &&
                c.Address.StreetNumber == customerDto.Address.StreetNumber &&
                c.Address.ZipCode == customerDto.Address.ZipCode &&
                c.Birthday == DateTime.Parse(customerDto.Birthday) &&
                c.Name == customerDto.Name &&
                c.Notes == customerDto.Notes &&
                c.Tags == string.Join(",", customerDto.Tags) &&
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
            var customerDto = Builders.GivenACustomerDto();

            var result = await Service.CreateNewCustomer(customerDto);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Erro ao criar um novo cliente. Por favor, contate o suporte."));
        }
    }
}