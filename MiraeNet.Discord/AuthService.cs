using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using MiraeNet.Discord.Networking.Rest;

namespace MiraeNet.Discord;

/// <summary>
///     A service for authenticating and obtaining tokens.
/// </summary>
public class AuthService(RestClient rest, ILogger<AuthService> logger)
{
    /// <summary>
    ///     Logs in with the specified login ID and password.
    /// </summary>
    /// <param name="login">The login ID to login with.</param>
    /// <param name="password">The password to login with.</param>
    /// <returns>The provisioned authentication token if login was successful.</returns>
    public async Task<string> LoginAsync(string login, string password)
    {
        logger.LogInformation("Obtaining authentication token.");
        var requestData = new Dictionary<string, dynamic>
        {
            { "login", login },
            { "password", password },
            { "undelete", false },
            { "login_source", null! },
            { "gift_code_sku_id", null! }
        };
        var response = await rest.Http.PostAsJsonAsync("auth/login", requestData);
        response.EnsureSuccessStatusCode();
        var responseData = await response.Content.ReadFromJsonAsync<Dictionary<string, JsonElement>>();
        var token = responseData!["token"].GetString();
        return token!;
    }
}