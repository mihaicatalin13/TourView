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

        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var locations = await _context.Locations.ToListAsync();
            return View(locations);
        }

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

        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<Location> locations = _context.Locations;

            if (!string.IsNullOrEmpty(searchString))
            {
                locations = locations.Where(l => l.Name.Contains(searchString));
            }

            return View(await locations.ToListAsync());
        }

    }
}
