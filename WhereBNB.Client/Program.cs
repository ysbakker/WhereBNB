using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Http;

namespace WhereBNB.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<ApiAuthorizationMessageHandler>();

            builder.Services.AddHttpClient("api-auth",
                    client => client.BaseAddress = new Uri("https://localhost:5001"))
                .AddHttpMessageHandler<ApiAuthorizationMessageHandler>();
            builder.Services.AddHttpClient("api", 
                client => client.BaseAddress = new Uri("https://localhost:5001"));

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("WhereBNB.API"));

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
                options.ProviderOptions.DefaultAccessTokenScopes.Add(
                    "https://wherebnb.onmicrosoft.com/f80c0b8e-d0d0-4e42-aa2e-86dd0aa5dd99/management");
                options.ProviderOptions.LoginMode = "redirect";
            });

            await builder.Build().RunAsync();
        }
    }
}