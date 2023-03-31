using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace E_Ticaret.Controllers
{

    public class SalesController : Controller
    {
        DataContext db = new DataContext();
        public IActionResult Index(int sayfa = 1)
        {
            var kullaniciadi = User.Identity?.Name;
            var kullanici = db.Users.FirstOrDefault(x => x.Name == kullaniciadi);
            var model = db.Sales.Include(x => x.Product).Include(x => x.Users).Where(x => x.UserId == kullanici.Id).ToList().ToPagedList(sayfa, 5);
            return View(model);
        }

        public ActionResult Buy(int id)
        {
            var model = db.Carts.Include(x => x.Product).FirstOrDefault(x => x.Id == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Buy2(int id)
        {

            if (ModelState.IsValid)
            {
                var model = db.Carts.Include(x => x.Product).FirstOrDefault(x => x.Id == id);
                var satis = new Sales
                {
                    UserId = model.UserId,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    Image = model.Image,
                    Price = model.Price,
                    Date = DateTime.Now,
                };
                db.Carts.Remove(model);
                db.Sales.Add(satis);
                db.SaveChanges();
                ViewBag.islem = "Satın alma işlemi başarılı bir şekilde gerçekleşmiştir.";
            }
            else
            {
                ViewBag.islem = "Satın alma işlemi başarısız.";
            }
            return View("islem");
        }
         
    
    public ActionResult Buyall(decimal? Tutar)
    {
        var kullaniciadi = User.Identity?.Name;
        var kullanici = db.Users.FirstOrDefault(x => x.Name == kullaniciadi);
        var model = db.Carts.Include(x => x.Product).Where(x => x.UserId == kullanici.Id).ToList();
        var kid = db.Carts.Include(x => x.Product).FirstOrDefault(x => x.UserId == kullanici.Id);
        if (model != null)
        {
            if (kid == null)
            {
                ViewBag.tutar = "Sepetinizde ürün bulunmamaktadır";
            }
            else if (kid != null)
            {
                Tutar = db.Carts.Include(x => x.Product).Where(x => x.UserId == kid.UserId).Sum(x => x.Product.Price * x.Quantity);
                ViewBag.Tutar = "Toplam Tutar=" + Tutar + "TL";

            }
            return View(model);
        }
        return View();

    }
    [HttpPost]

    public ActionResult Buyall2()
    {
        var username = User.Identity?.Name;
        var kullanici = db.Users.FirstOrDefault(x => x.Name == username);
        var model = db.Carts.Where(x => x.UserId == kullanici.Id).ToList();
        int row = 0;
        foreach (var item in model)
        {
            var satis = new Sales
            {
                UserId = model[row].UserId,
                ProductId = model[row].ProductId,
                Quantity = model[row].Quantity,
                Price = model[row].Price,
                Image = model[row].Image,
                Date = DateTime.Now,

            };
            db.Sales.Add(satis);
            db.SaveChanges();
            row++;
        }
        db.Carts.RemoveRange(model);
        db.SaveChanges();
        return RedirectToAction("Index", "Cart");
    }
}
}
