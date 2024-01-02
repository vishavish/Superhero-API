using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Superhero.Api.Extension
{
    public static class AuthExtension
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, string token)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = "https://localhost:44315/api/auth",
                    ValidAudience = "https://localhost:44315/api",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token))
                };
            });

            return services;
        }
    }
}
