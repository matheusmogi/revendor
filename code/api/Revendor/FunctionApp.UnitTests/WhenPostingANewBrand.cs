using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Revendor.Application.Services;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.FunctionApp.Brand;

namespace FunctionApp.UnitTests
{
    [TestFixture]
    public class WhenPostingANewBrand : TenantBaseTests<CreateBrandFunction,BrandDto>
    {
        private Mock<IBrandService> customerService;
        public override CreateBrandFunction Function { get; set; }

        protected override void AdditionalSetup()
        {
            customerService = new Mock<IBrandService>();
            customerService.Setup(x => x.CreateNewBrand(It.IsAny<BrandDto>()))
                .Returns(Task.FromResult((Result.Success())));

            Function = new CreateBrandFunction(customerService.Object, TokenIssue.Object);
        }

        [Test]
        public async Task ShouldInvokeBrandService()
        {
            var dto = new BrandDto
            {
                Id = "703F2861-3D89-401E-9F66-92765E52276E",
                Image = "imageBASE64",
                Name = "brand name",
                IsActive = true,
                IsPrivate = true,
                TenantId = TenantId
            };
            var request = BuildAnHttpRequest(Token, dto);

            await Function.RunAsync(request, null);

            customerService.Verify(x => x.CreateNewBrand(It.Is<BrandDto>(c =>
                c.Id == dto.Id &&
                c.Name == dto.Name &&
                c.Image == dto.Image &&
                c.IsActive == dto.IsActive &&
                c.IsPrivate == dto.IsPrivate &&
                c.TenantId == dto.TenantId
            )));
        }
    }
}