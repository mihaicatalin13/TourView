using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TourView.Data;
using TourView.Models;

namespace TourView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            Review fiveStarRev;
            int fiveStarLocId;
            Location location;
            if (_context.Reviews.FirstOrDefault(r => r.Rating == 5) != null)
            {
                fiveStarRev = _context.Reviews.OrderBy(rev => rev.Id).Last(rev => rev.Rating == 5);
                fiveStarLocId = _context.Reviews.OrderBy(rev => rev.Id).Last(rev => rev.Rating == 5).LocationId;
                location = _context.Locations.Find(fiveStarLocId);
                ViewBag.review = fiveStarRev;
                ViewBag.userName = _context.Users.First(u => u.Id == fiveStarRev.UserId).UserName;
                return View(location);
            }
            return View(null);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}