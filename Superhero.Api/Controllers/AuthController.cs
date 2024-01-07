using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Superhero.Api.Entities.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Superhero.Api.Models;
using Superhero.Api.Extension;

namespace Superhero.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ProblemDetails>> Login([FromBody] LoginModel login)
        {
            var user = await _userManager.FindByNameAsync(login.Username);
            if (user == null || !await _userManager.CheckPasswordAsync(user!, login.Password))
            {
                return Result<string>.Failure("Invalid username or password.").ToProblem(401);
            }

            var userRoles = await _userManager.GetRolesAsync(user!);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user!.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = GenerateToken(authClaims);
            return Ok(
                Result<TokenResponse>.Success(
                    new TokenResponse 
                    { 
                        Token = new JwtSecurityTokenHandler().WriteToken(token), 
                        Expiration = token.ValidTo 
                    }));
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ProblemDetails>> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return Ok(Result<string>.Success("Invalid username or password."));

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return Result<string>.Failure("Something happened while processing the record").ToProblem(500);

            await _userManager.AddToRoleAsync(user, Roles.User);

            return Ok(Result<string>.Success("User created!"));
        }

        private JwtSecurityToken GenerateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

    }
}