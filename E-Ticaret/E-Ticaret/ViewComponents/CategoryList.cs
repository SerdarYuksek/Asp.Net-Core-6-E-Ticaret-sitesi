using BusinessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.ViewComponents
{
    public class CategoryList:ViewComponent
    {
        CategoryRepository cm = new CategoryRepository();
        public IViewComponentResult Invoke ()
        {
            var values = cm.List();
            return View(values);
        }
    }
}
