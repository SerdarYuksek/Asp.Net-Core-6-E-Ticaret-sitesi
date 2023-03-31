using BusinessLayer.Concrete;
using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace E_Ticaret.Controllers
{
    public class AdminProductController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        DataContext db = new DataContext();

        public IActionResult Index(int sayfa=1)
        {
            var model = productRepository.GetUrunler();
            var model2 = productRepository.List().ToPagedList(sayfa,3);
            return View(model2);
        }

        public ActionResult Create()
        {
            List<SelectListItem> deger1=(from i in db.Categories.ToList()
                                          select new SelectListItem
                                          {
                                             Text=i.Name,
                                             Value=i.Id.ToString()


                                          }).ToList();
            ViewBag.ktgr = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product data, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hata oluştu");
            }
     
            data.Image = file.FileName.ToString();
            productRepository.Insert(data);
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/image/{data.Image}");
            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo (stream);
            
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var delete = productRepository.GetById(id);
            productRepository.Delete(delete);
            return RedirectToAction("Index");
        }

        public ActionResult Update(int id)
        {
            List<SelectListItem> deger1 = (from i in db.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.Name,
                                               Value = i.Id.ToString()


                                           }).ToList();
            ViewBag.ktgr = deger1;
            var update = productRepository.GetById(id);
            return View(update);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]

        public ActionResult Update(Product data, IFormFile file)
        {
            var update = productRepository.GetById(data.Id);
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Hata oluştu");
            }
            if (file == null)
            {

                update.Description = data.Description;
                update.Name = data.Name;
                update.IsApproved = data.IsApproved;
                update.Popular = data.Popular;
                update.Price = data.Price;
                update.Stock = data.Stock;
                update.CategoryId = data.CategoryId;
                productRepository.Update(update);
                return RedirectToAction("Index");
            }
            else
            {

                update.Description = data.Description;
                update.Name = data.Name;
                update.IsApproved = data.IsApproved;
                update.Popular = data.Popular;
                update.Price = data.Price;
                update.Stock = data.Stock;
                update.Image = file.FileName.ToString();
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/image/{update.Image}");
                using var stream = new FileStream(path, FileMode.Create);
                file.CopyTo(stream);
                update.CategoryId = data.CategoryId;
                productRepository.Update(update);
                return RedirectToAction("Index");
            }
        }
 
        public ActionResult CriticalStock()
        {
            var kritik = db.Products.Where(x => x.Stock <= 50).ToList();
            return View(kritik);
        }
       
    }
}
