using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace E_Ticaret.Controllers
{
    public class UserController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Update()
        {
            var username = (string)User.Identity?.Name;
            var değerler = db.Users.FirstOrDefault(x => x.Name == username);
            return View(değerler);
        }

        [HttpPost]

        public ActionResult Update(Users data)
        {
            var username = (string)User.Identity?.Name;
            var user = db.Users.Where(x => x.Name == username).FirstOrDefault();
            user.Name = data.Name;
            user.SurName = data.SurName;
            user.Email = data.Email;
            user.Password = data.Password;
            user.RePassword = data.RePassword;
            user.UserName = data.UserName;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PasswordReset(string eposta)
        {
            var mail = db.Users.Where(x => x.Email == eposta).SingleOrDefault();
            if (mail!=null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                Users sifre = new Users();
                mail.Password = (Convert.ToString(yenisifre));
                mail.RePassword = mail.Password;
                db.SaveChanges();
                ViewBag.uyari = "Şifreniz başarıyla güncellenmiştir.";
                ViewBag.sifre = yenisifre;
            }
            else
            {
                ViewBag.uyari = "Bir hata oluştu.Lütfen tekrar deneyiniz";
            }
            return View();
        }
    }
}
