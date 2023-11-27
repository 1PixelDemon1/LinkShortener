using Microsoft.AspNetCore.Mvc;
using ShortWeb.DataAccess.Data;
using ShortWeb.Model.Models;
using ShortWeb.Model.Models.ViewModels;

namespace ShortWeb.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(LinkIndexVM? shortLnk)
        {
            if (shortLnk is null || shortLnk!.link is null)
            {
                return View();
            }

            ShortLink? obj = _db.ShortLinks.FirstOrDefault(lnk => lnk.ShortenedLink == shortLnk.link.ToLower());
            
            if(obj is null)
            {
                return NotFound();
            }
            
            return Redirect(obj.Link);
        }        

        public IActionResult Privacy()
        {
            return View();
        }
    }
}