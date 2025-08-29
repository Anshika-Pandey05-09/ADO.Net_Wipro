// Here we will define a simple cookie-based authentication
// We will use a simple string and try to set it as a cookie

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Day35_OnlyCookieAuthentication.Controllers
{
   // [ApiController]
    public class CookieController : Controller
    {
        // To set a cookie go to /Cookie/SetCookie
        [HttpPost]
        public IActionResult SetCookie()
        {
            // Here we will set a simple cookie
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddMinutes(5);
            Response.Cookies.Append("MyCookie", "CookieValue", option);
            return Ok("Cookie has been set");
        }

        [HttpGet]
        public IActionResult GetCookie()
        {
            // Here we will retrieve the cookie
            if (Request.Cookies.TryGetValue("MyCookie", out string cookieValue))
            {
                return Ok($"Cookie value: {cookieValue}");
            }
            return NotFound("Cookie not found");
        }

        [HttpDelete]
        public IActionResult DeleteCookie()
        {
            // Here we will delete the cookie
            Response.Cookies.Delete("MyCookie");
            return Ok("Cookie has been deleted");
        }
    }
}