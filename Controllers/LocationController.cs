using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text.RegularExpressions;
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

            int _perPage = 3;
            if (TempData.ContainsKey("message"))
            {
                ViewBag.message = TempData["message"].ToString();
            }

            return View(await locations.ToListAsync());
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

        }
    }
