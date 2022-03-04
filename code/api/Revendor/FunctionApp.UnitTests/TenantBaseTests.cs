using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Revendor.Application.Authentication;
using Revendor.Domain.Dto;
using Revendor.FunctionApp;
using Revendor.FunctionApp.Customer;

namespace FunctionApp.UnitTests
{
    public abstract class TenantBaseTests<T, TDto> where TDto : TenantBaseDto, new()
                                                    where T : TenantBasedFunction<TDto>
    {
        public abstract T Function { get; set; }
        protected const string TenantId = "d93c7c90-e5e5-42b6-b29d-b25c9e55ec8d";

        protected string Token =
            "7/62veN1WgjAIBnUcsd6s69nA9E8CeE+3AXyT/LSV3F6imWe6a7uOYdd6IdtE68TkTOss9W8m8XxpBZiK9nQcNOOe6h7R/ua5XSaEZSquF60rwomCk7WEVoi5YIWN4OFJZ03030FWuaOkVqAmEUxIqo4xvzGeqMCzzp1wZmQDPiQpr50mO2dEY9XObPTuwuW0OA7YqW5OP8c1EDX3+2KA7H8fJK73OZiiSue8nN05Uw3ZU++NQ9FncoOnyZ0ClTQMG+jSRGlDLGf8Ff2dS/CUvVK2I2Tj7LkwQf2wbwrFzlgAmo66cGkjWKD2z1rw5ho5Pw+zL50KBpr4cTwVd5Sp2FEqQXq5KOlL6yOXzbygtU0/uN7M/GQ2QIVoxinerUAtvEIU+y/BwX7cBry6oE84BocSuo9r+34956rINfEVGW7OQL8Z3UowWBkj0TnFGUCKeTCcSfvhJjgDq0v3p7UY6oPuy/ybz5me3dJkHzBe+ptqCzG8GZnvBkfxHYodHsG";

        protected Mock<ITokenIssuer> TokenIssue;

        protected static readonly Dictionary<string, object> Claims = new()
        {
            ["tenant"] = TenantId,
            ["role"] = "Admin"
        };

        [SetUp]
        public void Setup()
        {
            TokenIssue = new Mock<ITokenIssuer>();
            TokenIssue.Setup(x =>
                    x.ExtractClaims(
                        "7/62veN1WgjAIBnUcsd6s69nA9E8CeE+3AXyT/LSV3F6imWe6a7uOYdd6IdtE68TkTOss9W8m8XxpBZiK9nQcNOOe6h7R/ua5XSaEZSquF60rwomCk7WEVoi5YIWN4OFJZ03030FWuaOkVqAmEUxIqo4xvzGeqMCzzp1wZmQDPiQpr50mO2dEY9XObPTuwuW0OA7YqW5OP8c1EDX3+2KA7H8fJK73OZiiSue8nN05Uw3ZU++NQ9FncoOnyZ0ClTQMG+jSRGlDLGf8Ff2dS/CUvVK2I2Tj7LkwQf2wbwrFzlgAmo66cGkjWKD2z1rw5ho5Pw+zL50KBpr4cTwVd5Sp2FEqQXq5KOlL6yOXzbygtU0/uN7M/GQ2QIVoxinerUAtvEIU+y/BwX7cBry6oE84BocSuo9r+34956rINfEVGW7OQL8Z3UowWBkj0TnFGUCKeTCcSfvhJjgDq0v3p7UY6oPuy/ybz5me3dJkHzBe+ptqCzG8GZnvBkfxHYodHsG"))
                .Returns(Claims);
            AdditionalSetup();
        }

        [Test]
        public async Task ShouldValidateTenantIdAuthorizationHeader()
        {
            var request = BuildAnHttpRequest();

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<UnauthorizedResult>(response);
        }

        [Test]
        public async Task ShouldValidateTenantIdAuthorizationHeaderWithoutBearer()
        {
            var request = BuildAnHttpRequest();
            request.Headers.Authorization = new AuthenticationHeaderValue("authorization");

            var response = await Function.RunAsync(request, null);

            Assert.IsInstanceOf<UnauthorizedResult>(response);
        }

        protected HttpRequestMessage BuildAnHttpRequest(string token = null, object requestContent = null)
        {
            var request = new HttpRequestMessage();
            if (requestContent != null)
            {
                request.Content = JsonContent.Create(requestContent);
                request.Content.Headers.ContentLength = requestContent.GetHashCode();
            }

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return request;
        }

        protected virtual void AdditionalSetup()
        {
        }
    }
}