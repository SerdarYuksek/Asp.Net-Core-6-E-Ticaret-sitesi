using DataAccessLayer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc.Core;
namespace E_Ticaret.Controllers
{
    
    public class AdminController : Controller
    {
        DataContext db = new DataContext();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Comment(int sayfa=1)
        {
            return View(db.Comments.Include(x => x.Product).Include(x => x.User).ToList().ToPagedList(sayfa,3));
        }
        public IActionResult Delete(int id)
        {
            var delete=db.Comments.Where(x=>x.Id==id).FirstOrDefault();
            db.Comments.Remove(delete);
            db.SaveChanges();
            return RedirectToAction("Comment");
        }

        public ActionResult UserList()
        {
            var user = db.Users.Where(x => x.Role == "User").ToList();
            return View(user);
        }

        public ActionResult UserDelete(int id)
        {
            var userId = db.Users.Where(x => x.Id == id).FirstOrDefault();
            db.Users.Remove(userId);
            db.SaveChanges();
            return RedirectToAction("UserList");
        }
    }
}
