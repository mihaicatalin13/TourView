using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourView.Data;
using TourView.Models;

namespace TourView.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ReservationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(int locationId)
        {
            DateTime now = DateTime.Now;
            // Taking reservations for specific location + not earlier than current time/date
            var reservations = await _context.Reservations.Where(r => r.LocationId == locationId && r.ReservationDate > now).ToListAsync();
            MyViewModel mvm = new MyViewModel();
            mvm.reservationsIEn = reservations;
            mvm.users = _context.Users;
            ViewData["location"] = _context.Locations.Find(locationId).Name;
            return View(mvm);
        }

        public IActionResult Create(int locationId)
        {
            ViewData["locationId"] = locationId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reservation reservation)
        {
            reservation.UserId = _userManager.GetUserId(User);

            _context.Add(reservation);
            _context.SaveChanges();
            return RedirectToAction("Details","Location", new { Id = reservation.LocationId });

        }

        public IActionResult SetSeen(int Id) 
        {
            int tempId = Id;
            var reservation = _context.Reservations.FirstOrDefault(r => r.Id == tempId);
            reservation.seen = true;
            _context.SaveChanges();
            return RedirectToAction("Index", new { locationId = reservation.LocationId});
        }
    }
}
