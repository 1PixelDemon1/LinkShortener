using Microsoft.AspNetCore.Mvc;
using ShortDataAccess.Data;
using ShortModel.Models;
using ShortModel.Models.ViewModels;

namespace ShortWeb.Areas.User.Controllers
{
    [Area("User")]
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

            ShortLink? obj = _db.ShortLinks.FirstOrDefault(lnk => lnk.ShortenedLink == shortLnk.link);
            
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