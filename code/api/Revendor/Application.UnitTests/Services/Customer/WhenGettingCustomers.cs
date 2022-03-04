using System;
using System.Linq;
using NUnit.Framework;
using Revendor.Application.Services;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using SharedTests;

namespace Application.UnitTests.Services.Customer
{
    [TestFixture]
    public class WhenGettingCustomers : ServiceTestBase<CustomerService>
    {
        private readonly CustomerDto fakeCustomer = Builders.GivenACustomerDto();
        protected override CustomerService Service { get; set; }

        protected override void AdditionalSetup()
        {
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Customer>())
                .Returns(new[] { new Revendor.Domain.Entities.Customer(fakeCustomer) }.AsQueryable());
            Service = new CustomerService(Repository.Object, Logger.Object);
        }

        [Test]
        public void ShouldGetCustomersFromRepository()
        {
            Service.GetCustomersByTenantId(fakeCustomer.TenantId);

            Repository.Verify(x => x.Query<Revendor.Domain.Entities.Customer>());
        }

        [Test]
        public void ShouldConvertEntityToDtoAndReturn()
        {
            var result = Service.GetCustomersByTenantId(fakeCustomer.TenantId);

            Assert.IsInstanceOf<Result>(result);
            var customer = result.ResponseObject as CustomerDto[];
            Assert.That(customer?.First().Address.City, Is.EqualTo(fakeCustomer.Address.City));
            Assert.That(customer?.First().Address.Complement, Is.EqualTo(fakeCustomer.Address.Complement));
            Assert.That(customer?.First().Address.Neighbourhood, Is.EqualTo(fakeCustomer.Address.Neighbourhood));
            Assert.That(customer?.First().Address.State, Is.EqualTo(fakeCustomer.Address.State));
            Assert.That(customer?.First().Address.AddressLine, Is.EqualTo(fakeCustomer.Address.AddressLine));
            Assert.That(customer?.First().Address.StreetNumber, Is.EqualTo(fakeCustomer.Address.StreetNumber));
            Assert.That(customer?.First().Address.ZipCode, Is.EqualTo(fakeCustomer.Address.ZipCode));
            Assert.That(customer?.First().Birthday, Is.EqualTo(DateTime.Parse(fakeCustomer.Birthday).ToString("yyyy-MM-dd")));
            Assert.That(customer?.First().Id, Is.EqualTo(fakeCustomer.Id));
            Assert.That(customer?.First().Name, Is.EqualTo(fakeCustomer.Name));
            Assert.That(customer?.First().Notes, Is.EqualTo(fakeCustomer.Notes));
        }
    }
}