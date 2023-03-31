using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace E_Ticaret.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        ProductRepository productRepository = new ProductRepository();
        public IActionResult Index(int sayfa=1)
        {
            return View(productRepository.List().ToPagedList(sayfa, 3));
        }

       
    }
}
