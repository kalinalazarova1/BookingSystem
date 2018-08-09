using BookingSystem.Filters;
using BookingSystem.Models;
using BookingSystem.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Controllers
{
    [Produces("application/json")]
    public class AuthController : Controller
    {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> hasher;
        private IConfiguration configuration;

        public AuthController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> hasher, IConfiguration configuration)
        {
            this.hasher = hasher;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost("api/auth/token")]
        [ValidateModel]
        public async Task<IActionResult> CreateToken([FromBody] CredentialsModelcs model)
        {
            try
            {
                var user = await this.userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, model.Username)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Tokens:Key"]));
                    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                    if (this.hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var token = new JwtSecurityToken(
                        issuer: this.configuration["Tokens:Issuer"],
                        audience: this.configuration["Tokens:Issuer"],
                        claims: claims,
                        expires: DateTime.UtcNow.AddHours(2),
                        signingCredentials: credentials);

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return BadRequest();
        }
    }
}
