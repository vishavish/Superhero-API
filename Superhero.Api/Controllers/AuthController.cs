using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Superhero.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("get-token")]
        public async Task<IActionResult> GetToken()
        {
            return Ok(GenerateToken("sample"));
        }

        private string GenerateToken(string username)
        {
            //TODO: Replace "secretKey" with actual token from appsettings.json
            const string secretKey = "THIS IS MY SECRET KEY: appsecret123789";

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = creds,
                Issuer = "https://localhost:44315/api/auth",
                Audience = "https://localhost:44315/api"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

    }
}