using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Revendor.Application.Authentication;
using Revendor.Domain.Dto;
using Revendor.Domain.Interfaces.Services;

namespace Revendor.FunctionApp.InventoryMovement
{
    public class CreateInventoryMovementFunction : TenantBasedFunction<InventoryMovementDto>
    {
        private readonly IInventoryMovementService movementService;

        public CreateInventoryMovementFunction(IInventoryMovementService movementService, ITokenIssuer tokenIssuer) : base(tokenIssuer)
        {
            this.movementService = movementService;
        }

        [FunctionName("NewInventoryMovement")]
        public override async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "inventoryMovement")] HttpRequestMessage req, ILogger log)
        {
            return await ProcessRequest(req, async (requestContent) =>
            {
                var response = await movementService.AddNewMovement(requestContent);
                return ActionResultBuilder.Build(response);
            });
        }
        
    }
}