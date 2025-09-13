namespace OAuthServerAPI.Model
{
    public class TokenRequest
    {
        public string? grant_type { get; set; }
        public string? client_id { get; set; }
        public string? client_secret { get; set; }
        public string? scope { get; set; }
    }

    public record ClientConfig
    {
        public string ClientId { get; init; } = "";
        public string ClientSecret { get; init; } = "";
    }

}
