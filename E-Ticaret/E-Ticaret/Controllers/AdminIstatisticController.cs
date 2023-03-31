using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class AdminIstatisticController : Controller
    {
        DataContext db = new DataContext();
        public IActionResult Index()
        {
            var satis = db.Sales.Count();
            ViewBag.satis = satis;

            var urun = db.Products.Count();
            ViewBag.urun = urun;

            var kategori = db.Categories.Count();
            ViewBag.kategori = kategori;

            var sepet = db.Carts.Count();
            ViewBag.sepet = sepet;

            var User = db.Users.Count();
            ViewBag.kullanici = User;
            return View();
        }
    }
}
