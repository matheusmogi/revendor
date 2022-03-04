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
    public class WhenDeletingANewCustomer : TenantBaseTests<DeleteCustomerFunction, CustomerDto>
    {
        private Mock<ICustomerService> customerService;

        public override DeleteCustomerFunction Function { get; set; }

        protected override void AdditionalSetup()
        {
            customerService = new Mock<ICustomerService>();
            customerService.Setup(x => x.DeleteCustomer(It.IsAny<string>()))
                .Returns(Task.FromResult(Result.Success()));

            Function = new DeleteCustomerFunction(customerService.Object, TokenIssue.Object);
        }

        [Test]
        public async Task ShouldInvokeCustomerService()
        {
            var dto = Builders.GivenACustomerDto();
            var request = BuildAnHttpRequest(Token, dto);

            await Function.RunAsync(request, null);

            customerService.Verify(x => x.DeleteCustomer(It.Is<string>(c => c == dto.Id)));
        }

        [Test]
        public async Task ShouldReturnOkObjectResultWhenCreateNewUser()
        {
            var dto = Builders.GivenACustomerDto();
            var request = BuildAnHttpRequest(Token, dto);

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task ShouldReturnBadRequestObjectResultWhenCreateNewUser()
        {
            customerService.Setup(x => x.DeleteCustomer(It.IsAny<string>()))
                .Returns(Task.FromResult((Result.Failure("fake failure"))));
            Function = new DeleteCustomerFunction(customerService.Object, TokenIssue.Object);
            var request = BuildAnHttpRequest(Token, new CustomerDto());

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.That(((response as BadRequestObjectResult)?.Value as string[] ?? Array.Empty<string>()).First(), Is.EqualTo("fake failure"));
        }
    }
}