using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Interfaces.Services;
using Revendor.FunctionApp.InventoryMovement;

namespace FunctionApp.UnitTests
{
    [TestFixture]
    public class WhenAddingANewInventoryMovement : TenantBaseTests<CreateInventoryMovementFunction, InventoryMovementDto>
    {
        private Mock<IInventoryMovementService> inventoryMovementService;
        public override CreateInventoryMovementFunction Function { get; set; }

        protected override void AdditionalSetup()
        {
            inventoryMovementService = new Mock<IInventoryMovementService>();
            inventoryMovementService.Setup(x => x.AddNewMovement(It.IsAny<InventoryMovementDto>()))
                .Returns(Task.FromResult((Result.Success())));

            Function = new CreateInventoryMovementFunction(inventoryMovementService.Object, TokenIssue.Object);
        }

        [Test]
        public async Task ShouldInvokeService()
        {
            var dto = new InventoryMovementDto { Quantity = 2, ProductId = "product-id", TenantId = TenantId };
            var request = BuildAnHttpRequest(Token, dto);

            await Function.RunAsync(request, null);

            inventoryMovementService.Verify(x => x.AddNewMovement(It.Is<InventoryMovementDto>(c =>
                c.Quantity == dto.Quantity &&
                c.Id == dto.Id &&
                c.TenantId == (string)Claims["tenant"]
            )));
        }

        [Test]
        public async Task ShouldReturnOkObjectResultWhenCreateNewUser()
        {
            var dto = new InventoryMovementDto { Quantity = 2, ProductId = "product-id", TenantId = TenantId };
            var request = BuildAnHttpRequest(Token, dto);

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<OkObjectResult>(response);
        }

        [Test]
        public async Task ShouldReturnBadRequestObjectResultWhenCreateNewUser()
        {
            inventoryMovementService.Setup(x => x.AddNewMovement(It.IsAny<InventoryMovementDto>()))
                .Returns(Task.FromResult((Result.Failure("fake failure"))));
            Function = new CreateInventoryMovementFunction(inventoryMovementService.Object, TokenIssue.Object);
            var request = BuildAnHttpRequest(Token, new CustomerDto());

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.That(((response as BadRequestObjectResult)?.Value as string[] ?? Array.Empty<string>()).First(), Is.EqualTo("fake failure"));
        }
    }
}