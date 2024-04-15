using BlogApp_Net8.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlogApp_Net8.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogApp_Net8.Controllers
{
    public class LoginController : Controller
    {
        private readonly BlogDbContext _context;
        public LoginController(BlogDbContext context)
        {
            _context = context;
        }

        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                User user = await _context.Users.Where(u => u.Username ==  loginViewModel.UserName).FirstOrDefaultAsync();

                if(user != null){
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index","Home");

                }

            }

            return RedirectToAction("AccessDenied");
        }

        public async Task<IActionResult> logout()
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("login");
        }

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
