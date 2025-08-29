// here we will generate jwt and send it as response with the same token value

using JwtAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JwtController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public JwtController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


// To get this go to /api/jwt
        [HttpPost]
        public IActionResult GenerateToken([FromBody] UserModel userModel)
        {
            if (userModel == null || string.IsNullOrEmpty(userModel.Email) || string.IsNullOrEmpty(userModel.Password))
            {
                return BadRequest("Invalid client request");
            }

            // Validate the user credentials (this is just a simple example, in a real application you would check the credentials against a database)
            // if (userModel.Email == "test@example.com" && userModel.Password == "password")
            // {
            //     var token = GenerateJwtToken(userModel);
            //     return Ok(new { token });
            // }
            // For demonstration, we will accept any non-empty email and password
            var token = GenerateJwtToken(userModel);
            return Ok(new { token });
        }

        private string GenerateJwtToken(UserModel userModel)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userModel.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)); // ! mark gives surity of not null
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}