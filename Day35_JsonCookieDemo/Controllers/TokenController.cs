using Day35_JsonCookieDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtCookieDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TokenController : ControllerBase
    {
        private readonly string _secretKey = "your_secret_key_here_efrfrfdgdfdhdgngfnfnfg"; // replace with secure key -- Length of secret key should be min 16 characters for HmacSha256
        private readonly string _issuer = "your_issuer_here";
        private readonly string _audience = "your_audience_here";

        // To generate token go on /Token/GenerateToken in Postman and for username and role use the body send in JSON format eg: { "username": "testuser", "role": "Admin" }
        // For curl command
        // curl -X POST https://localhost:5001/Token/GenerateToken -H "Content-Type: application/json" -d "{ \"username\": \"testuser\", \"role\": \"Admin\" }"
        [HttpPost]
        public IActionResult GenerateToken([FromBody] TokenRequest request)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, request.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // 🔹 Store JWT in HttpOnly cookie
            Response.Cookies.Append("jwt", tokenString, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // set true in production (requires HTTPS)
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddMinutes(30)
            });

            return Ok(new { message = "Token generated and stored in cookie." });
        }

        // Validate token from cookie go on /Token/ValidateToken in Postman

        [HttpPost]
        public IActionResult ValidateToken()
        {
            if (!Request.Cookies.TryGetValue("jwt", out string? token))
            {
                return Unauthorized(new { valid = false, message = "No token found in cookie." });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                return Ok(new { valid = true });
            }
            catch
            {
                return Unauthorized(new { valid = false, message = "Invalid or expired token." });
            }
        }


        // For refreshing token go on /Token/RefreshToken in Postman
        [HttpPost]
        public IActionResult RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("jwt", out string? token))
            {
                return Unauthorized(new { valid = false, message = "No token found in cookie." });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _issuer,
                    ValidAudience = _audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);

                // Create a new token
                var newToken = new JwtSecurityToken(
                    issuer: _issuer,
                    audience: _audience,
                    claims: ((JwtSecurityToken)validatedToken).Claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );

                var newTokenString = new JwtSecurityTokenHandler().WriteToken(newToken);

                // Replace cookie
                Response.Cookies.Append("jwt", newTokenString, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.Now.AddMinutes(30)
                });

                return Ok(new { message = "Token refreshed and stored in cookie." });
            }
            catch
            {
                return Unauthorized(new { valid = false, message = "Invalid or expired token." });
            }
        }

        // For logging out go on /Token/Logout in Postman
        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "Token deleted from cookie (logged out)." });
        }
    }
}