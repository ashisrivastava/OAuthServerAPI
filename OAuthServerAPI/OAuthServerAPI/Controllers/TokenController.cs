

namespace OAuthServerAPI.Controllers
{
    using OAuthServerAPI.Model;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("connect")]
    public class TokenController : ControllerBase
    {
        private readonly ClientStore _clients;
        private readonly TokenService _tokens;

        public TokenController(ClientStore clients, TokenService tokens)
        {
            _clients = clients;
            _tokens = tokens;
        }

        [HttpPost("token")]
        [Consumes("application/x-www-form-urlencoded")]
        public IActionResult Token([FromForm] TokenRequest req)
        {
            if (req.grant_type != "client_credentials")
                return BadRequest(new { error = "unsupported_grant_type" });

            if (string.IsNullOrEmpty(req.client_id) || string.IsNullOrEmpty(req.client_secret))
                return BadRequest(new { error = "invalid_client" });

            var client = _clients.ValidateClient(req.client_id!, req.client_secret!);
            if (client == null)
                return Unauthorized();

            var scopes = (req.scope ?? string.Empty).Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var token = _tokens.CreateToken(client.ClientId, scopes);

            return Ok(token);
        }
    }
}
