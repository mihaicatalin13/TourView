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
            Location location = _context.Locations.First(loc => loc.Id == locationId);
            ViewData["location"] = location.Name;
            ViewData["locationId"] = location.Id;
            MyViewModel myViewModel = new MyViewModel();
            myViewModel.users = _context.Users;
            myViewModel.reviewsIEn = reviews;
            return View(myViewModel);
        }

        public IActionResult Create(int locationId)
        {
            ViewBag.locationId = locationId;
            Location location = _context.Locations.First(loc => loc.Id == locationId);
            ViewData["location"] = location.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Review review)
        {
            ApplicationUser user = _context.Users.First(u => u.UserName == User.Identity.Name);
            review.User = user;
            review.UserId = user.Id;
            review.Location = _context.Locations.Find(review.LocationId);
            _context.Add(review);
            _context.SaveChanges();
            return RedirectToAction("Index", new { locationId = review.LocationId });
        
            Location location = _context.Locations.First(loc => loc.Id == review.LocationId);
            ViewData["location"] = location.Name;
            return View(review);
        }

        public async Task<IActionResult> Edit(int? reviewId)
        {
            if (reviewId == null || reviewId == 0)
            {
                return NotFound();
            }
            var reviewFromDb = await _context.Reviews.FirstOrDefaultAsync(rev => rev.Id == reviewId);
            if (reviewFromDb == null)
            {
                return NotFound();
            }
            ViewData["reviewId"] = reviewId;
            var locationId = _context.Reviews.Find(reviewId).LocationId;
            ViewData["locationId"] = locationId;
            ViewData["location"] = _context.Locations.Find(locationId).Name;
            return View(reviewFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int reviewId, Review review)
        {
            var id = reviewId;
            review.User = _context.Users.First(u => u.UserName == User.Identity.Name);
            review.Id = id;

            _context.Update(review);
            _context.SaveChanges();

            return RedirectToAction("Index", new { locationId = review.LocationId });

            //return RedirectToAction("Edit", new { reviewId = review.Id });
        }

        public async Task<IActionResult> Delete(int? reviewId)
        {
            if (reviewId == null || reviewId == 0)
            {
                return NotFound();
            }
            var reviewFromDb = await _context.Reviews.FirstOrDefaultAsync(rev => rev.Id == reviewId);
            if (reviewFromDb == null)
            {
                return NotFound();
            }
            ViewData["reviewId"] = reviewId;
            var locationId = _context.Reviews.Find(reviewId).LocationId;
            ViewData["locationId"] = locationId;
            ViewData["location"] = _context.Locations.Find(locationId).Name;
            return View(reviewFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int reviewId)
        {
            var review = _context.Reviews.Find(reviewId);
            if (review == null)
            {
                return NotFound();
            }
            _context.Remove(review);
            _context.SaveChanges();
            return RedirectToAction("Index", new { locationId = review.LocationId });


        }
    }
}