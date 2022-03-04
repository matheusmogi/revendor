using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Revendor.Application.Authentication;
using Revendor.Domain.Dto;
using Revendor.Domain.Interfaces.Services;

namespace Revendor.FunctionApp.Product
{
    public class GetProductsFunction : TenantBasedFunction<ProductDto>
    {
        private readonly IProductService productService;

        public GetProductsFunction(IProductService productService, ITokenIssuer tokenIssuer) : base(tokenIssuer)
        {
            this.productService = productService;
        }

        [FunctionName("GetProducts")]
        public override async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequestMessage req, ILogger log)
        {
            return await ProcessRequest(req, async (productDto) =>
            {
                if (productDto.TenantId != null)
                {
                    var productsByTenantId = productService.GetProductsByTenantId(productDto.TenantId);
                    return await Task.FromResult(ActionResultBuilder.Build(productsByTenantId));
                }
                
                //TODO: get all products regardless the tenant
                var response = productService.GetProductsByTenantId(productDto.TenantId);
                return await Task.FromResult(ActionResultBuilder.Build(response));
            });
        }
    }
}