namespace OAuthServerAPI
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public class TokenService
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly byte[] _signingKey;
        private readonly int _lifetimeMinutes;

        public TokenService(string issuer, string audience, string signingKey, int lifetimeMinutes)
        {
            _issuer = issuer;
            _audience = audience;
            _signingKey = Encoding.UTF8.GetBytes(signingKey);
            _lifetimeMinutes = lifetimeMinutes;
        }

        public object CreateToken(string clientId, string[] scopes)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, clientId),
            new Claim("client_id", clientId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            if (scopes?.Length > 0)
                claims.Add(new Claim("scope", string.Join(' ', scopes)));

            var key = new SymmetricSecurityKey(_signingKey);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_lifetimeMinutes),
                signingCredentials: creds
            );

            var handler = new JwtSecurityTokenHandler();
            return new
            {
                access_token = handler.WriteToken(jwt),
                token_type = "Bearer",
                expires_in = (int)TimeSpan.FromMinutes(_lifetimeMinutes).TotalSeconds
            };
        }
    }

}
