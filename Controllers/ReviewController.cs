using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourView.Data;
using TourView.Models;

namespace TourView.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(int locationId)
        {
            IEnumerable<Review> reviews = await _context.Reviews.Where(r => r.LocationId == locationId).ToListAsync();
            ViewBag.locationId = locationId;
            return View(reviews);
        }

        public IActionResult Create(int locationId)
        {
            ViewBag.locationId = locationId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            review.UserId = _userManager.GetUserId(User);
            

            _context.Add(review);
            _context.SaveChanges();
            return RedirectToAction("Index", new { locationId = review.LocationId });
        }
    }
}