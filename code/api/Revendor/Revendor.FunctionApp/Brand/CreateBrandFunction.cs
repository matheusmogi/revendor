using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Revendor.Application.Authentication;
using Revendor.Application.Services;
using Revendor.Domain.Dto;

namespace Revendor.FunctionApp.Brand
{
    public class CreateBrandFunction : TenantBasedFunction<BrandDto>
    {
        private readonly IBrandService brandService;

        public CreateBrandFunction(IBrandService brandService, ITokenIssuer tokenIssuer) : base(tokenIssuer)
        {
            this.brandService = brandService;
        }

        
        [FunctionName("NewBrand")]
        public override async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "brand")] HttpRequestMessage req, ILogger log)
        {
            return await ProcessRequest(req, async (requestContent) =>
            {
                var response = await brandService.CreateNewBrand(requestContent);
                return ActionResultBuilder.Build(response);
            });
        }
    }
}