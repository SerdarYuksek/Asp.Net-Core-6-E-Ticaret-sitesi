using DataAccessLayer.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace E_Ticaret.Controllers
{
    public class AdminSalesController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index(int sayfa=1)
        {

            return View(db.Sales.Include(x => x.Product).ToList().ToPagedList(sayfa, 5));
        }
    }
}
