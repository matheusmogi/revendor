using Microsoft.AspNetCore.Mvc;
using Revendor.Domain.Common;

namespace Revendor.FunctionApp
{
    public static class ActionResultBuilder
    {
        public static IActionResult Build(Result response)
        {
            if (response.Succeeded)
            {
                return new OkObjectResult(response.ResponseObject ?? new { message = "request processed successfully." });
            }

            return new BadRequestObjectResult(response.Errors);
        }
    }
}