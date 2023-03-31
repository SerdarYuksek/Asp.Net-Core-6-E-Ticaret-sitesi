using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository categoryRepository = new CategoryRepository();
     
        public ActionResult Details(int id)
        {
            var cat = categoryRepository.CategoryDetails(id);
            return View(cat);
        }
    }
}
