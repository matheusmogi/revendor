using System;
using System.Linq;
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
    public class WhenPatchingAProduct : TenantBaseTests<UpdateProductFunction, ProductDto>
    {
        private Mock<IProductService> productService;
        public override UpdateProductFunction Function { get; set; }

        protected override void AdditionalSetup()
        {
            productService = new Mock<IProductService>();
            productService.Setup(x => x.UpdateProduct(It.IsAny<ProductDto>()))
                .Returns(Task.FromResult((Result.Success())));

            Function = new UpdateProductFunction(productService.Object, TokenIssue.Object);
        }

        [Test]
        public async Task ShouldInvokeProductService()
        {
            var dto = Builders.GivenAProductDto();
            var request = BuildAnHttpRequest(Token, dto);

            await Function.RunAsync(request, null);

            productService.Verify(x => x.UpdateProduct(It.Is<ProductDto>(c =>
                c.Id == dto.Id &&
                c.Code == dto.Code &&
                c.Name == dto.Name &&
                c.Description == dto.Description &&
                c.Brand.Id == dto.Brand.Id &&
                c.TenantId == (string)Claims["tenant"]
            )));
        }

        [Test]
        public async Task ShouldReturnOkObjectResultWhenUpdatingAnUser()
        {
            var dto = Builders.GivenAProductDto();
            var request = BuildAnHttpRequest(Token, dto);

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task ShouldReturnBadRequestObjectResultWhenUpdatingAnUser()
        {
            productService.Setup(x => x.UpdateProduct(It.IsAny<ProductDto>()))
                .Returns(Task.FromResult((Result.Failure("fake failure"))));
            Function = new UpdateProductFunction(productService.Object, TokenIssue.Object);
            var request = BuildAnHttpRequest(Token, new ProductDto());

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.That(((response as BadRequestObjectResult)?.Value as string[] ?? Array.Empty<string>()).First(), Is.EqualTo("fake failure"));
        }
    }
}