using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.Api.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class AuthController : ControllerBase
        {
            private readonly IConfiguration _config;

            public AuthController(IConfiguration config)
            {
                _config = config;
            }

            [HttpPost("login")]
            public IActionResult Login([FromBody] LoginDto login)
            {
                if (login.Username == "admin" && login.Password == "123")
                {
                    var token = GenerateJwtToken(login.Username);
                    return Ok(new { token });
                }
                return Unauthorized();
            }

            private string GenerateJwtToken(string username)
            {
                var jwtSettings = _config.GetSection("Jwt");

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

                var token = new JwtSecurityToken(
                    issuer: jwtSettings["Issuer"],
                    audience: jwtSettings["Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }

        public class LoginDto
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }

