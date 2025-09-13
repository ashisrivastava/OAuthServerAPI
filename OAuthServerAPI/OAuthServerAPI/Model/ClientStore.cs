namespace OAuthServerAPI.Model
{
    using System.Security.Cryptography;
    using System.Text;

    public class ClientStore
    {
        private readonly Dictionary<string, string> _clients = new();

        public ClientStore(IEnumerable<ClientConfig> configs)
        {
            foreach (var c in configs)
                if (!string.IsNullOrEmpty(c.ClientId))
                    _clients[c.ClientId] = c.ClientSecret ?? "";
        }

        public ClientConfig? ValidateClient(string clientId, string clientSecret)
        {
            if (!_clients.TryGetValue(clientId, out var expectedSecret))
                return null;

            if (!CryptographicOperations.FixedTimeEquals(
                Encoding.UTF8.GetBytes(expectedSecret),
                Encoding.UTF8.GetBytes(clientSecret)))
                return null;

            return new ClientConfig { ClientId = clientId, ClientSecret = expectedSecret };
        }
    }

}
