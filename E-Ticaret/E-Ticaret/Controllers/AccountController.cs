using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace E_Ticaret.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        DataContext db = new DataContext();

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task <ActionResult> Login(Users data )
        {
            var bilgiler = db.Users.FirstOrDefault(x => x.Email == data.Email && x.Password == data.Password);
            if (bilgiler != null)
            {
                var Claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,bilgiler.Name),
                    new Claim(ClaimTypes.Role,bilgiler.Role),
                    new Claim(ClaimTypes.Email,data.Email),

                };
                var useridentity = new ClaimsIdentity(Claims, "a");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                ViewBag.hata = "Mail veya Şifreniz hatalıdır";
                return View(data);
            }
        }

        [HttpPost]
        public ActionResult Register (Users data)
        {
            if (ModelState.IsValid)
            {
                
                db.Users.Add(data);
                data.Role = "User";
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("", "Hatalı");
            return View("Login", data);
        }

        [HttpGet]
        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
