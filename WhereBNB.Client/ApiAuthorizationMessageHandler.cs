using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace WhereBNB.Client
{
    public class ApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        private static string scope = "https://wherebnb.onmicrosoft.com/f80c0b8e-d0d0-4e42-aa2e-86dd0aa5dd99/management";
        public ApiAuthorizationMessageHandler(IAccessTokenProvider provider, 
            NavigationManager navigationManager)
            : base(provider, navigationManager)
        {
            ConfigureHandler(
                new[] { "https://localhost:5001", "https://wherebnb-api.azurewebsites.net" },
                new[] { scope });
        }
    }
}