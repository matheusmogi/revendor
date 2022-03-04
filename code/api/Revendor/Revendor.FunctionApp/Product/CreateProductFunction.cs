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
    public class CreateProductFunction : TenantBasedFunction<ProductDto>
    {
        private readonly IProductService productService;

        public CreateProductFunction(IProductService productService, ITokenIssuer tokenIssuer) : base(tokenIssuer)
        {
            this.productService = productService;
        }

        [FunctionName("NewProduct")]
        public override async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "product")] HttpRequestMessage req, ILogger log)
        {
            return await ProcessRequest(req, async (requestContent) =>
            {
                var response = await productService.CreateNewProduct(requestContent);
                return ActionResultBuilder.Build(response);
            });
        }
        
    }
}