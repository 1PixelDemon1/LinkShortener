using Microsoft.AspNetCore.Mvc;
using ShortWeb.DataAccess.Data;
using ShortWeb.Model.Models;
using System.Diagnostics;

namespace ShortWeb.Areas.User.Controllers
{
    [Area("User")]
    public class CreateLinkController : Controller
    {

        private readonly ApplicationDbContext _db;

        public CreateLinkController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ShortLink? obj) {
            if(obj is null) {
                return NotFound();
            }
            
            var lnk = _db.ShortLinks.FirstOrDefault(sl => sl.Link == obj.Link);
            if (lnk is not null)
            {
                return View(lnk);
            }

            obj.ShortenedLink = GenerateShortLink();
            
            _db.ShortLinks.Add(obj);
            _db.SaveChanges();
            return View(obj);
        }

        private string GenerateShortLink()
        {
            string result = string.Empty;
            int tries = 10;
            int linkLength = 5;
            while (true)
            {
                while (tries-- > 0)
                {
                    string shortLink = Guid.NewGuid().ToString().Substring(0, linkLength);
                    if (_db.ShortLinks.FirstOrDefault(sl => sl.ShortenedLink == shortLink) is null)
                    {
                        result = shortLink;
                        break;
                    }
                }
                if (tries == 0)
                {
                    linkLength++;
                }
                else
                {
                    break;
                }
            }
            return result;
        }
    }
}