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
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminPanelController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
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

        public async Task<IActionResult> AllLocations()
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

        public async Task<IActionResult> RolesPanel()
        {
            MyViewModel mvm = new MyViewModel();
            IEnumerable<ApplicationUser> users = await _context.Users.ToListAsync();
            mvm.usersIEn = users;
            
            var userRoles = await _context.UserRoles.ToListAsync();
            var roles = await _context.Roles.ToListAsync();
            var userWithRoles = from userRole in userRoles
                                join role in roles on userRole.RoleId equals role.Id
                                join user in users on userRole.UserId equals user.Id into userGroup
                                from user in userGroup.DefaultIfEmpty()
                                select new
                                {
                                    UserId = userRole.UserId,
                                    RoleId = userRole.RoleId,
                                    RoleName = role.Name,
                                    UserName = user != null ? user.UserName : string.Empty
                                };
            ViewBag.userWithRoles = userWithRoles;
            return View(mvm);
        }

        public async Task<IActionResult> SetManager(string userId)
        {
            ApplicationUser user = await _context.Users.FindAsync(userId);
            await _userManager.AddToRoleAsync(user, "Editor");    // Change to manager when manager is added
            return RedirectToAction("RolesPanel");
        }

        
    }
}
