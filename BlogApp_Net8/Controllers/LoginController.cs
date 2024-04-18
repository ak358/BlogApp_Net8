using BlogApp_Net8.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlogApp_Net8.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp_Net8.Controllers
{
    public class LoginController : Controller
    {
        private readonly BlogDbContext _context;
        public LoginController(BlogDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public IActionResult login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]//書かなくてもPOSTメソッドには自動適用される
        public async Task<IActionResult> login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                User user = await _context.Users.Where(u => u.Username ==  loginViewModel.UserName).FirstOrDefaultAsync();

                if(user != null){
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, (user.Id).ToString()),
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

                    return RedirectToAction("Index", "Articles");

                }

            }

            return RedirectToAction("AccessDenied");
        }

        [Authorize]
        public async Task<IActionResult> logout()
        {
            // Clear the existing external cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [AllowAnonymous]

        public IActionResult AccessDenied()
        {
            return View("AccessDenied");
        }
    }
}
