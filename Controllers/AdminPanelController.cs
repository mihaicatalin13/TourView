using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TourView.Data;
using TourView.Models;

namespace TourView.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminPanelController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            MyViewModel mvm = new MyViewModel();

            IEnumerable<ApplicationUser> users = await _context.Users.ToListAsync();
            mvm.usersIEn = users;
            IEnumerable<Location> locations = await _context.Locations.ToListAsync();
            mvm.locationsIEn = locations.Reverse();
            IEnumerable<Review> reviews = await _context.Reviews.ToListAsync();
            mvm.reviewsIEn = reviews.Reverse();

            return View(mvm);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllReviews()
        {
            MyViewModel mvm = new MyViewModel();
            IEnumerable<ApplicationUser> users = await _context.Users.ToListAsync();
            mvm.usersIEn = users;
            IEnumerable<Location> locations = await _context.Locations.ToListAsync();
            mvm.locationsIEn = locations.Reverse();
            IEnumerable<Review> reviews = await _context.Reviews.ToListAsync();
            mvm.reviewsIEn = reviews.Reverse();
            return View(mvm);
        }

    }
}
