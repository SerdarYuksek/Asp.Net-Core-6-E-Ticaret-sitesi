using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.ViewComponents
{
    public class PopularProduct : ViewComponent
    {
        ProductRepository pm = new ProductRepository();
        public IViewComponentResult Invoke()
        {
            var product =pm.GetPopularProduct();
            ViewBag.popular = product;
            return View("PopularProduct");
        }
    }
}
