using System.Net.Http.Json;
using System.Text.Json;
using MiraeNet.Discord.Rest;

namespace MiraeNet.Discord;

public class AuthService(RestClient rest)
{
    public async Task<string> LoginAsync(string login, string password)
    {
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