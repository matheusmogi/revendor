using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Revendor.Application.Authentication;
using Revendor.Domain.Dto;
using Revendor.Domain.Interfaces.Services;

namespace Revendor.FunctionApp.User
{
    public class UserFunction
    {
        private readonly IUserService userService;

        public UserFunction(IUserService userService)
        {
            this.userService = userService;
        }

        [FunctionName("NewUser")]
        public async Task<IActionResult> NewUserAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "user")] HttpRequestMessage req, ILogger log)
        {
            var requestContent = await req.Content.ReadAsAsync<UserDto>();
            var response = await userService.CreateNewUser(requestContent);

            return response.Succeeded ? new OkObjectResult(response.ResponseObject) as IActionResult : new BadRequestObjectResult(response.Errors);
        }
        
        [FunctionName("Login")]
        public async Task<IActionResult> LoginAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "login")] HttpRequestMessage req, ILogger log)
        {
            var requestContent = await req.Content.ReadAsAsync<LoginDto>();
            var response = userService.SignInUser(requestContent.Username, requestContent.Password);

            return response.Succeeded ? new OkObjectResult(response.ResponseObject) as IActionResult : new BadRequestObjectResult(response.Errors);
        }
    }
}