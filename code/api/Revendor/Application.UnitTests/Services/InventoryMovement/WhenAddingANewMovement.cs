using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using Revendor.Domain.Dto;

namespace Application.UnitTests.Services.InventoryMovement
{
    public class WhenAddingANewMovement : ServiceTestBase<InventoryMovementService>
    {
        protected override InventoryMovementService Service { get; set; }

        protected override void AdditionalSetup()
        {
            Service = new InventoryMovementService(Repository.Object, Logger.Object);
        }

        [Test]
        public async Task ShouldAddNewMovementInDataBase()
        {
            GivenAProductInRepository();
            var dto = new InventoryMovementDto("productId", 3, "tenantId");

            await Service.AddNewMovement(dto);

            Repository.Verify(x => x.AddAsync(It.Is<Revendor.Domain.Entities.InventoryMovement>(i =>
                i.ProductId == dto.ProductId &&
                i.Quantity == dto.Quantity &&
                i.TenantId == dto.TenantId &&
                i.CreatedAt.Date == DateTime.Now.Date &&
                !string.IsNullOrEmpty(i.Id)
            )));

            Repository.Verify(x => x.SaveChangesAsync());
        }

        [TestCase(1, 3)]
        [TestCase(-1, 1)]
        public async Task ShouldDecreaseQuantityOfTheProduct(int movementQuantity, int finalCount)
        {
            GivenAProductInRepository();
            var dto = new InventoryMovementDto("productId", movementQuantity, "tenantId");

            await Service.AddNewMovement(dto);

            Repository.Verify(x => x.Update(It.Is<Revendor.Domain.Entities.Product>(s =>
                s.CurrentInventory == finalCount &&
                s.Id == dto.ProductId
            )));
        }
        

        [TestCase("productId", "fakeTenant")]
        [TestCase("fakeProduct", "tenantId")]
        public async Task ShouldThrowErrorWhenProductDoesntExists(string productId, string tenantId)
        {
            GivenAProductInRepository();
            var dto = new InventoryMovementDto(productId, 1, tenantId);
            
          var result =  await Service.AddNewMovement(dto);
          
          Assert.False(result.Succeeded);
          Assert.That(result.Errors.First(), Is.EqualTo("Produto não encontrado. Por favor, contate o suporte."));
        }

        [Test]
        public async Task ShouldReturnErrorWhenSomethingFail()
        {
            GivenAProductInRepository();
            Repository.Setup(x => x.SaveChangesAsync()).Throws<Exception>();
            var dto = new InventoryMovementDto("productId", 3, "tenantId");


            var result = await Service.AddNewMovement(dto);

            Assert.False(result.Succeeded);
            Assert.That(result.Errors.First(), Is.EqualTo("Erro ao movimentar o estoque. Por favor, contate o suporte."));
        }
        
        private void GivenAProductInRepository()
        {
            Repository.Setup(x => x.Query<Revendor.Domain.Entities.Product>())
                .Returns(new Revendor.Domain.Entities.Product[]
                {
                    new()
                    {
                        Id = "productId",
                        TenantId = "tenantId",
                        CurrentInventory = 2
                    }
                }.AsQueryable);
        }
    }
}