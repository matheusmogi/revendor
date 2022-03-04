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
    public class DeleteProductFunction : TenantBasedFunction<ProductDto>
    {
        private readonly IProductService productService;

        public DeleteProductFunction(IProductService productService, ITokenIssuer tokenIssuer) : base(tokenIssuer)
        {
            this.productService = productService;
        }

        [FunctionName("DeleteProduct")]
        public override async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "product/delete")] HttpRequestMessage req, ILogger log)
        {
            return await ProcessRequest(req, async (requestContent) =>
            {
                var response = await productService.DeleteProduct(requestContent.Id);
                return ActionResultBuilder.Build(response);
            });
        }
        
    }
}