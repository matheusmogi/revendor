using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Revendor.Application.Authentication;
using Revendor.Domain.Dto;
using Revendor.Domain.Interfaces.Services;

namespace Revendor.FunctionApp.Customer
{
    public class DeleteCustomerFunction : TenantBasedFunction<CustomerDto>
    {
        private readonly ICustomerService customerService;

        public DeleteCustomerFunction(ICustomerService customerService, ITokenIssuer tokenIssuer) : base(tokenIssuer)
        {
            this.customerService = customerService;
        }

        [FunctionName("DeleteCustomer")]
        public override async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "customer/delete")] HttpRequestMessage req, ILogger log)
        {
            return await ProcessRequest(req, async (requestContent) =>
            {
                var response = await customerService.DeleteCustomer(requestContent.Id);
                return ActionResultBuilder.Build(response);
            });
        }
    }
}