// Creating three controller:
// AccountController.cs : This controller will helps us manage user accounts
// TokenController.cs : This controller will helps us manage JWT tokens, so UI is not required 
//                      and it will be stateless
// SecureController.cs : This controller will helps us manage secure endpoints via JWT tokens
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace JwtCookieDemo.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SecureController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        // This attribute is used to protect the endpoint
        //Here we can implement a secure endpoint that requires a valid JWT token like below 
        //CookieOnly() : authenticated User
        //AdminOnly() : Admin role required
        //HRonly() : HR role required
        //JWTonly() : JWT token required
        public IActionResult CookieOnly()
        {
            return Ok(new { data = "This is cookie-only secure data" });
        }
        [HttpGet("/Secure/AdminOnly")]
        [Authorize(Roles = "Admin")] // Role based access
        public IActionResult AdminOnly()
        {
            return Ok(new { data = "This is admin-only secure data" });
        }
        [HttpGet("/Secure/HRonly")]
        [Authorize(Roles = "HR")] // Role based access
        public IActionResult HRonly()
        {
            return Ok(new { data = "This is HR-only secure data" });
        }
        [HttpGet("/Secure/JWTonly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        //this is JWT only
        public IActionResult JWTonly()
        {
            return Ok(new { data = "This is JWT-only secure data" });
        }
    }
}