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

        public LocationController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize(Roles = "User, Editor, Admin")]
        public async Task<IActionResult> Index(string name, string? description, string? address)
        {
            ViewData["CurrentName"] = name;
            ViewData["CurrentDescription"] = description;
            ViewData["CurrentAddress"] = address;

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

            var location = await _context.Locations.FirstOrDefaultAsync(m => m.Id == id);

            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }


        [Authorize(Roles = "Editor")]
        public IActionResult Create()
        {
            string idManager = _userManager.GetUserId(User);
            int locationExists = _context.Locations.Where(m => m.ManagerId == idManager).ToList().Count();
            
            if (locationExists > 0)
            {
                TempData["message"] = "You already have a location";
                int idLoc = _context.Locations.FirstOrDefault(m => m.ManagerId == idManager)?.Id ?? 0;
                return RedirectToAction("Details", "Location", new { id = idLoc });

            }
            else
            {
                Location location = new Location();
                return View(location);
            }
        }
     

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Create(Location location)
        {
            location.ManagerId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                TempData["message"] = "Location loaded";
                return RedirectToAction(nameof(Index));
            }
            return View(location);


        }


        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var location = await _context.Locations.FindAsync(id);
            if (location == null) { return NotFound(); }

            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Address,PhoneNumber,Schedule,Menu,PhotoUrl,Rating")] Location location)
        {
            if (id != location.Id) { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id)) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        [Authorize(Roles = "Editor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var location = await _context.Locations.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null) { return NotFound(); }

            return View(location);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Editor")]
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