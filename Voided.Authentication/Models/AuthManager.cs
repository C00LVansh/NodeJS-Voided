using System.Text.Json;
using Voided.Authentication.Enums;

namespace Voided.Authentication.Models
{
    public class AuthManager
    {
        private readonly HttpClient _httpClient = new();

        private const string BaseUrl = "https://voided.to/auth.php";

        private const string Salt = "5939e31c-c460-41a2-92e4-da21f1e3ca93";

        /// <summary>
        /// Autheticates a user with a given key, provider and required usergroup.
        /// </summary>
        /// <param name="key">Authentication key provided by the user.</param>
        /// <param name="provider">Provider provided by the developer.</param>
        /// <param name="requiredGroup">The required mininum usergroup to use the tool.</param>
        /// <returns></returns>
        public async Task<AuthResponse> AuthenticateAsync(string key, string provider, Usergroup requiredGroup)
        {
            try
            {
                var url = $"{BaseUrl}?key={Uri.EscapeDataString(key)}&provider={Uri.EscapeDataString(provider)}";
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("PKey", Salt);

                using var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return new AuthResponse()
                    {
                        Authenticated = false,
                        Message = "Authentication failed. Invalid key.",
                        User = null
                    };
                }

                var body = await response.Content.ReadAsStringAsync();

                var authKeyResponse = JsonSerializer.Deserialize<AuthKey>(body, AuthKeyJsonContext.Default.AuthKey)
                           ?? throw new Exception("Unexpected response.");

                var expiration = DateTimeOffset.FromUnixTimeSeconds(authKeyResponse.SaltTime).DateTime;

                if (expiration < DateTime.Now)
                {
                    return new AuthResponse()
                    {
                        Authenticated = false,
                        Message = "Authentication failed. Salt has expired.",
                        User = null
                    };
                }

                var user = new User()
                {
                    Id = authKeyResponse.Uid,
                    Username = authKeyResponse.Username,
                    Usergroup = (Usergroup)authKeyResponse.UsergroupId,
                    Expiration = expiration,
                    Salt = authKeyResponse.ProviderSalt
                };

                if (user.Usergroup >= requiredGroup)
                {
                    return new AuthResponse()
                    {
                        Authenticated = true,
                        Message = "Authentication successful! User has been authenticated.",
                        User = user
                    };
                }
                else
                {
                    return new AuthResponse()
                    {
                        Authenticated = false,
                        Message = "Authentication failed! User doesn't have the required usergroup.",
                        User = user
                    };
                }
            }
            catch (Exception e)
            {
                return new AuthResponse()
                {
                    Authenticated = false,
                    Message = $"Authentication failed. Unexpected exception: {e.Message}",
                    User = null
                };
            }
        }
    }
}