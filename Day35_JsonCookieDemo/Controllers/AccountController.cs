// AccountController.cs : This controller will helps us manage user accounts
//here i am going to implemnt quick demo via querystring( No UI): Sets a cookie with role + Claims
//eg: ?role=Admin&name=John --- testing reference
//eg: ?role=Employee&name=Smith&empnumber=12345 --- testing reference
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace JwtCookieDemo.Controllers
{
    [ApiController] // this means it will automatically validate the model state and it is API
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        // For cookieLogin go on /Account/CookieLogin?role=Admin&name=John in Postman and in body send eg: { "empnumber": "12345" }
        [HttpGet]
        public async Task<IActionResult> CookieLogin([FromQuery] User user)
        {
            var claims = new List<Claim> // create a list of claims
    {
        new Claim(ClaimTypes.Role, user.Role), // user role
        new Claim("name", user.Name), // user name
        new Claim("empnumber", user.EmpNumber) // employee number
    };
            var claimsIdentity = new ClaimsIdentity(claims, "cookie"); // create a claims identity
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity); // create a claims principal
            await HttpContext.SignInAsync("cookie", claimsPrincipal); // sign in the user with cookie authentication
                                                                      // so this method will set a cookie in the user's browser so that the user remains authenticated with the
                                                                      //  help of cookie authentication
                                                                      // Limitation of this approach is that it is not stateless and relies on cookies which are created on the server
                                                                      // If the cookie is deleted or expires, the user will be logged out by default lifetime of a cookie is 14 days
            return Ok();
        }
    }
}