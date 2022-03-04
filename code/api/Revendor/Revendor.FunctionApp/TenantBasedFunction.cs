using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Revendor.Application.Authentication;
using Revendor.Domain.Dto;
using Revendor.Domain.ValueObjects;

namespace Revendor.FunctionApp
{
    public abstract class TenantBasedFunction<T> where T : TenantBaseDto, new()
    {
        private readonly ITokenIssuer tokenIssuer;

        protected TenantBasedFunction(ITokenIssuer tokenIssuer)
        {
            this.tokenIssuer = tokenIssuer;
        }

        public abstract Task<IActionResult> RunAsync(HttpRequestMessage req, ILogger log);

        protected async Task<IActionResult> ProcessRequest(HttpRequestMessage req, Func<T, Task<IActionResult>> processAction)
        {
            if (req.Headers.Authorization?.Parameter == null && req.Headers.Authorization?.Scheme != "Bearer")
            {
                return new UnauthorizedResult();
            }

            var claims = tokenIssuer.ExtractClaims(req.Headers.Authorization.Parameter);

            var tenantId = claims["tenant"]?.ToString();
            if (!Equals(Enum.Parse<Role>(claims["role"].ToString()), Role.Admin) && tenantId == null)
            {
                return new UnauthorizedResult();
            }

            var requestContent = req.Content.Headers.ContentLength > 0 ? await req.Content.ReadAsAsync<T>() : new T();
            requestContent.TenantId = tenantId;
            return await processAction(requestContent);
        }
    }
}