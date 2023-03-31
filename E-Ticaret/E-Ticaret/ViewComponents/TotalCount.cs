using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.ViewComponents
{
    public class TotalCount : ViewComponent
    {

        DataContext db = new DataContext();
        public IViewComponentResult Invoke(int? count)
        {
            var model = db.Users.FirstOrDefault(x => x.Name == User.Identity.Name);
            count = db.Carts.Include(x => x.Product).Where(x => x.UserId == model.Id).Count();
            ViewBag.count = count;
            if (count == 0)
            {
                ViewBag.count = null;
            }
            return View();
        }
    }

}
