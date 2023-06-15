using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TourView.Data;
using TourView.Models;

namespace TourView.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IWebHostEnvironment _env;

        public LocationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _env = env;
        }

        [HttpGet]
        [Authorize(Roles = "User, Editor, Admin")]
        public async Task<IActionResult> Index(string name, string? description, string? address)
        {
            ViewData["CurrentName"] = name;
            ViewData["CurrentDescription"] = description;
            ViewData["CurrentAddress"] = address;
            ViewBag.reviews = await _context.Reviews.ToListAsync();

            var locations = from b in _context.Locations
                            select b;
            if (!String.IsNullOrEmpty(name))
            {
                locations = locations.Where(b => b.Name.Contains(name));
            }
            if (!String.IsNullOrEmpty(description))
            {
                locations = locations.Where(b => b.Description.Contains(description));
            }
            if (!String.IsNullOrEmpty(address))
            {
                locations = locations.Where(b => b.Address.Contains(address));
            }

            return View(await locations.ToListAsync());
        }

        [Authorize(Roles = "User, Editor, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location location = await _context.Locations.FirstOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }
            MyViewModel mvm = new MyViewModel();
            mvm.location = location;
            mvm.reservationsIEn = _context.Reservations.Where(r => r.LocationId == location.Id)
                                                        .Where(r => r.ReservationDate > DateTime.Now);
            string locationManager = _context.Users.First(u => u.Id == location.ManagerId).Id;
            ViewBag.managerName = _context.Users.First(u => u.Id == locationManager).UserName;
            return View(mvm);
        }


        [Authorize(Roles = "Editor")]
        public IActionResult Create()
        {
            string userId = _userManager.GetUserId(User);
            try
            {
                int? loc = _context.Locations.First(l => l.ManagerId == userId).Id;
                return RedirectToAction("Details", new { id = loc });
            }
            catch (Exception)
            {
                return View();
            }          

        }
     

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Create(Location location, IFormFile locationImage)
        {
            location.ManagerId = _userManager.GetUserId(User);
            if (locationImage != null && locationImage.Length > 0)  
            {
                var storagePath = Path.Combine(
                    _env.WebRootPath,
                    "images",
                    locationImage.FileName);
                var databaseFileName = "/images/" + locationImage.FileName;
                using(var fileStream = new FileStream(storagePath, FileMode.Create))
                {
                    await locationImage.CopyToAsync(fileStream);
                }
                location.PhotoUrl = databaseFileName;
            }


            if (ModelState.IsValid) 
            {
                
                _context.Add(location);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(location);


        }


        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var locationFromDb = await _context.Locations.FirstOrDefaultAsync(loc => loc.Id == id);
            if (locationFromDb == null)
            {
                return NotFound();
            }
            ViewData["locationId"] = id;
            return View(locationFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> EditPOST(int id, IFormFile locationImage, [Bind("Id,Name,Description,Address,PhoneNumber,Schedule,Menu,PhotoUrl,Rating")] Location location)
        {
            location.Id = id;
            location.ManagerId = _context.Users.First(u => u.UserName == User.Identity.Name).Id;
            if (locationImage.Length > 0)
            {
                var storagePath = Path.Combine(
                    _env.WebRootPath,
                    "images",
                    locationImage.FileName);
                var databaseFileName = "/images/" + locationImage.FileName;
                using (var fileStream = new FileStream(storagePath, FileMode.Create))
                {
                    await locationImage.CopyToAsync(fileStream);
                }
                location.PhotoUrl = databaseFileName;
            }
            if (ModelState.IsValid)
            {
                _context.Update(location);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(location);
        }

        [Authorize(Roles = "Editor,Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var location = await _context.Locations.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null) { return NotFound(); }

            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor,Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var location = await _context.Locations.FindAsync(id);
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LocationExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}