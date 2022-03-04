using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Revendor.Application.Authentication;
using Revendor.Domain.Common;
using Revendor.Domain.Dto;
using Revendor.Domain.Interfaces.Services;

namespace Revendor.FunctionApp.Customer
{
    public class GetCustomersFunction : TenantBasedFunction<CustomerDto>
    {
        private readonly ICustomerService customerService;

        public GetCustomersFunction(ICustomerService customerService, ITokenIssuer tokenIssuer) : base(tokenIssuer)
        {
            this.customerService = customerService;
        }

        [FunctionName("GetCustomers")]
        public override async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "customers")] HttpRequestMessage req, ILogger log)
        {
            return await ProcessRequest(req, async (customerDto) =>
            {
                if (customerDto.TenantId != null)
                {
                    var customersByTenantId = customerService.GetCustomersByTenantId(customerDto.TenantId);
                    return await Task.FromResult(ActionResultBuilder.Build(customersByTenantId));
                }
                
                //TODO: get all customers regardless the tenant
                var response = customerService.GetCustomersByTenantId(customerDto.TenantId);
                return await Task.FromResult(ActionResultBuilder.Build(response));
            });
        }
    }
}