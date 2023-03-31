using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        DataContext db = new DataContext();
      
        public ActionResult ProductDetails(int id)
        {
            var details = productRepository.GetById(id);
            details.Category = db.Categories.FirstOrDefault(x => x.Id == details.CategoryId);
            var yorum = db.Comments.Include(x => x.Product).Include(x => x.User).Where(x => x.ProductId == id).ToList();
            ViewBag.yorum = yorum;
            return View(details);

        }
        [HttpPost]
        public ActionResult Comment(Comment data)
        {
                db.Comments.Add(data);
                data.Product = db.Products.FirstOrDefault(x => x.Id == data.ProductId);
                data.User = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
           
        }
    }
}
