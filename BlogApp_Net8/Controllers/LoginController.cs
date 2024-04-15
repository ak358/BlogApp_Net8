using BlogApp_Net8.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlogApp_Net8.Data;

namespace BlogApp_Net8.Controllers
{
    public class LoginController : Controller
    {
        private readonly BlogDbContext _context;
        public LoginController(BlogDbContext context)
        {
            context = _context;
        }

        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult login(LoginViewModel loginViewModel)
        {

            //if (ModelState.IsValid)
            //{
            //    string username = 
            //    string password =
                


            //    var claims = new List<Claim>
            //    {
            //        new Claim(ClaimTypes.Name, user.Email),
            //        new Claim("FullName", user.FullName),
            //        new Claim(ClaimTypes.Role, "Administrator"),
            //    };

            //    var claimsIdentity = new ClaimsIdentity(
            //        claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //    var authProperties = new AuthenticationProperties
            //    {
            //        //AllowRefresh = <bool>,
            //        // Refreshing the authentication session should be allowed.

            //        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            //        // The time at which the authentication ticket expires. A 
            //        // value set here overrides the ExpireTimeSpan option of 
            //        // CookieAuthenticationOptions set with AddCookie.

            //        //IsPersistent = true,
            //        // Whether the authentication session is persisted across 
            //        // multiple requests. When used with cookies, controls
            //        // whether the cookie's lifetime is absolute (matching the
            //        // lifetime of the authentication ticket) or session-based.

            //        //IssuedUtc = <DateTimeOffset>,
            //        // The time at which the authentication ticket was issued.

            //        //RedirectUri = <string>
            //        // The full path or absolute URI to be used as an http 
            //        // redirect response value.
            //    };

            //    await HttpContext.SignInAsync(
            //        CookieAuthenticationDefaults.AuthenticationScheme,
            //        new ClaimsPrincipal(claimsIdentity),
            //        authProperties);

            //    _logger.LogInformation("User {Email} logged in at {Time}.",
            //        user.Email, DateTime.UtcNow);

            //    return LocalRedirect(Url.GetLocalUrl(returnUrl));
            //}


            return View();
        }

        public async Task<IActionResult> logout()
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return View("login");

        }

    }
}
