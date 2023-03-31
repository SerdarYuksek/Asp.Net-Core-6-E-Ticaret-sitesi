using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class CartController : Controller
    {
     
        DataContext db = new DataContext();
        public IActionResult Index(decimal? Tutar)
        {
                var username = User.Identity?.Name;
                var kullanici = db.Users.FirstOrDefault(x => x.Name == username);
                var model = db.Carts.Include(x => x.Product).Where(x => x.UserId == kullanici.Id).ToList();
                var kid = db.Carts.Include(x => x.Product).FirstOrDefault(x => x.UserId == kullanici.Id);
                if (model!=null)
                {
                    if (kid==null)
                    {
                        ViewBag.Tutar = "Sepetinizde ürün bulunmamaktadır!";
                    }
                    else if (kid!=null)
                    {
                        Tutar = db.Carts.Include(x => x.Product).Where(x => x.UserId == kid.UserId).Sum(x=>x.Product.Price * x.Quantity);
                        ViewBag.Tutar = "Toplam Tutar=" + Tutar + "TL";
                    }
                    return View(model);
                }
            return View();
        }
        public ActionResult AddCart(int id)
        {
                var kullaniciadi = User.Identity?.Name;
                var model = db.Users.FirstOrDefault(x => x.Name == kullaniciadi);
                var u = db.Products.Find(id);
                var sepet = db.Carts.Include(x => x.Product).FirstOrDefault(x => x.UserId == model.Id && x.Product.Id == id);
                if (model!=null)
                {
                    if (sepet!=null)
                    {
                        sepet.Quantity++;
                        sepet.Price = u.Price * sepet.Quantity;
                        db.SaveChanges();
                        return RedirectToAction("Index", "Cart");
                    }
                    var s = new Cart
                    {
                        UserId = model.Id,
                        ProductId = u.Id,
                        Image = u.Image,
                        Quantity = 1,
                        Price = u.Price,
                        Date = DateTime.Now
                    };

                    db.Carts.Add(s);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cart");
                } 
            
            return View();
        }

        public void DinamikMiktar(int id,int miktari)
        {
            var model = db.Carts.Find(id);
            model.Quantity = miktari;
            model.Price = model.Price * model.Quantity;
            db.SaveChanges();
        }

        public ActionResult Azalt(int id)
        {
            var model = db.Carts.Find(id);
            if (model.Quantity==1)
            {
                db.Carts.Remove(model);
                db.SaveChanges();
            }
            model.Quantity--;
            model.Price = model.Price * model.Quantity;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Arttir(int id)
        {
            var model = db.Carts.Find(id);
            model.Quantity++;
            model.Price = model.Price * model.Quantity;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Delete(int id)
        {
            var sil = db.Carts.Find(id);
            db.Carts.Remove(sil);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteRange()
        {
                var kullanici = User.Identity?.Name;
                var model = db.Users.FirstOrDefault(x => x.Name == kullanici);
                var sil = db.Carts.Where(x => x.UserId == model.Id);
                db.Carts.RemoveRange(sil);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }
    }
}
