using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Interfaces.Services;
using Revendor.FunctionApp.Customer;
using SharedTests;

namespace FunctionApp.UnitTests.Customers
{
    [TestFixture]
    public class WhenPatchingACustomer : TenantBaseTests<UpdateCustomerFunction, CustomerDto>
    {
        private Mock<ICustomerService> customerService;
        public override UpdateCustomerFunction Function { get; set; }

        protected override void AdditionalSetup()
        {
            customerService = new Mock<ICustomerService>();
            customerService.Setup(x => x.UpdateCustomer(It.IsAny<CustomerDto>()))
                .Returns(Task.FromResult((Result.Success())));

            Function = new UpdateCustomerFunction(customerService.Object, TokenIssue.Object);
        }

        [Test]
        public async Task ShouldInvokeCustomerService()
        {
            var dto = Builders.GivenACustomerDto();
            var request = BuildAnHttpRequest(Token, dto);

            await Function.RunAsync(request, null);

            customerService.Verify(x => x.UpdateCustomer(It.Is<CustomerDto>(c =>
                c.Address.City == dto.Address.City &&
                c.Address.Complement == dto.Address.Complement &&
                c.Address.Neighbourhood == dto.Address.Neighbourhood &&
                c.Address.State == dto.Address.State &&
                c.Address.AddressLine == dto.Address.AddressLine &&
                c.Address.StreetNumber == dto.Address.StreetNumber &&
                c.Address.ZipCode == dto.Address.ZipCode &&
                c.Birthday == dto.Birthday &&
                c.Id == dto.Id &&
                c.Name == dto.Name &&
                c.Notes == dto.Notes &&
                c.TenantId == (string)Claims["tenant"]
            )));
        }

        [Test]
        public async Task ShouldReturnOkObjectResultWhenUpdatingAnUser()
        {
            var dto = Builders.GivenACustomerDto();
            var request = BuildAnHttpRequest(Token, dto);

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task ShouldReturnBadRequestObjectResultWhenUpdatingAnUser()
        {
            customerService.Setup(x => x.UpdateCustomer(It.IsAny<CustomerDto>()))
                .Returns(Task.FromResult((Result.Failure("fake failure"))));
            Function = new UpdateCustomerFunction(customerService.Object, TokenIssue.Object);
            var request = BuildAnHttpRequest(Token, new CustomerDto());

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.That(((response as BadRequestObjectResult)?.Value as string[] ?? Array.Empty<string>()).First(), Is.EqualTo("fake failure"));
        }
    }
}