using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Interfaces.Services;
using Revendor.FunctionApp.Product;
using SharedTests;

namespace FunctionApp.UnitTests.Products
{
    [TestFixture]
    public class WhenGettingProducts : TenantBaseTests<GetProductsFunction,ProductDto>
    {
        private Mock<IProductService> productService;
        public override GetProductsFunction Function { get; set; }

        protected override void AdditionalSetup()
        {
            productService = new Mock<IProductService>();
            productService.Setup(x => x.CreateNewProduct(It.IsAny<ProductDto>()))
                .Returns(Task.FromResult((Result.Success())));

            Function = new GetProductsFunction(productService.Object, TokenIssue.Object);
        }

        [Test]
        public void ShouldInvokeProductServicePassingTenantId()
        {
            var request = BuildAnHttpRequest(Token, new{});
            
            Function.RunAsync(request, null);

            productService.Verify(x => x.GetProductsByTenantId((string)Claims["tenant"]));
        }
        
        [Test]
        public async Task ShouldReturnOkObjectResultWhenCreateNewUser()
        {
            productService
                .Setup(x => x.GetProductsByTenantId((string)Claims["tenant"]))
                .Returns(Result.Success(Builders.GivenAProductDto()));
            var request = BuildAnHttpRequest(Token, new{});

            var response =  await Function.RunAsync(request, null);
           
            Assert.IsInstanceOf<OkObjectResult>(response);
        }
    }
}