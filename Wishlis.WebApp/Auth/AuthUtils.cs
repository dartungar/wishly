using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Wishlis.WebApp.Auth;

public static class AuthUtils
{
    public static Task OnRedirectToIdentityProviderForSignOut(RedirectContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        context.ProtocolMessage.Scope = "openid";
        context.ProtocolMessage.ResponseType = "code";

        var cognitoDomain = configuration["Cognito:Domain"];
        var clientId = configuration["Cognito:ClientId"];
        var appSignOutUrl = configuration["Cognito:AppSignOutUrl"];

        var logoutUrl = $"{context.Request.Scheme}://{context.Request.Host}{appSignOutUrl}";

        context.ProtocolMessage.IssuerAddress = $"{cognitoDomain}/logout?client_id={clientId}" +
                                                $"&logout_uri={logoutUrl}" +
                                                $"&redirect_uri={logoutUrl}";

        // delete cookies
        context.Properties.Items.Remove(CookieAuthenticationDefaults.AuthenticationScheme);
        // close openid session
        context.Properties.Items.Remove(OpenIdConnectDefaults.AuthenticationScheme);

        return Task.CompletedTask;
    }
}