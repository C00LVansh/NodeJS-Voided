using System.Text.Json.Serialization;

namespace Voided.Authentication.Models
{
    public class AuthKey
    {
        [JsonPropertyName("uid")]
        public int Uid { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("usergroup")]
        public int UsergroupId { get; set; }

        [JsonPropertyName("provider_salt")]
        public string ProviderSalt { get; set; } = string.Empty;

        [JsonPropertyName("salt_time")]
        public long SaltTime { get; set; }
    }

    [JsonSerializable(typeof(AuthKey))]
    internal partial class AuthKeyJsonContext : JsonSerializerContext
    {
    }
}