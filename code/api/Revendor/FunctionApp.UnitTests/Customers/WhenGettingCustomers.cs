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
    public class WhenGettingCustomers : TenantBaseTests<GetCustomersFunction,CustomerDto>
    {
        private Mock<ICustomerService> customerService;
        public override GetCustomersFunction Function { get; set; }

        protected override void AdditionalSetup()
        {
            customerService = new Mock<ICustomerService>();
            customerService.Setup(x => x.CreateNewCustomer(It.IsAny<CustomerDto>()))
                .Returns(Task.FromResult((Result.Success())));

            Function = new GetCustomersFunction(customerService.Object, TokenIssue.Object);
        }

        [Test]
        public void ShouldInvokeCustomerServicePassingTenantId()
        {
            var request = BuildAnHttpRequest(Token, new{});
            
            Function.RunAsync(request, null);

            customerService.Verify(x => x.GetCustomersByTenantId((string)Claims["tenant"]));
        }
        
        [Test]
        public async Task ShouldReturnOkObjectResultWhenCreateNewUser()
        {
            customerService
                .Setup(x => x.GetCustomersByTenantId((string)Claims["tenant"]))
                .Returns(Result.Success(Builders.GivenACustomerDto()));
            var request = BuildAnHttpRequest(Token, new{});

            var response =  await Function.RunAsync(request, null);
           
            Assert.IsInstanceOf<OkObjectResult>(response);
        }
    }
}