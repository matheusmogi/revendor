using System.IO;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Revendor.Application;
using Revendor.FunctionApp;
using Revendor.Infrastructure;

[assembly: FunctionsStartup(typeof(Startup))]

namespace Revendor.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddLogging(a=>a.AddApplicationInsights(configuration["APPINSIGHTS_INSTRUMENTATIONKEY"]));
            builder.Services.AddInfrastructure(configuration);
            builder.Services.AddApplication();
        }
    }
}