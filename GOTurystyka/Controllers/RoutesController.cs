using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GOTurystyka.Models;

namespace GOTurystyka.Controllers
{
    public class RoutesController : Controller
    {
        private readonly GOTurystykaContext _context;

        public RoutesController(GOTurystykaContext context)
        {
            _context = context;
        }

        // GET: Routes
        public async Task<IActionResult> Index()
        {
            var gOTurystykaContext = _context.Routes.Include(r => r.Creator);
            return View(await gOTurystykaContext.ToListAsync());
        }

        // GET: Routes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            var points = await _context.SegmentsInRoutes
                .Where(sir => sir.RouteId == id)
                .Include(sir => sir.Segment)
                .Include(sir => sir.Segment.PointsInSegments)
                .SelectMany(sir => sir.Segment.PointsInSegments)
                .Include(pis => pis.Point)
                .Select(pis => pis.Point)
                .ToListAsync();

          
            if (route == null)
            {
                return NotFound();
            }

            return View((route, points));
        }

        // GET: Routes/Create
        public IActionResult Create()
        {
            ViewData["CreatorId"] = new SelectList(_context.Tourists, "Id", "Email");
            return View();
        }

        // POST: Routes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NumberOfPoints,CreatorId")] Route route)
        {
            route.AlreadyTravelled = false;
            route.Approved = false;
            route.LastUpdate = DateTime.Now;
            route.DateOfCreation = DateTime.Now;
            TryValidateModel(route);
            if (ModelState.IsValid)
            {
                _context.Add(route);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.Tourists, "Id", "Email", route.CreatorId);
            return View(route);
        }

        // GET: Routes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes.FindAsync(id);
            if (route == null)
            {
                return NotFound();
            }
            ViewData["CreatorId"] = new SelectList(_context.Tourists, "Id", "Email", route.CreatorId);
            return View(route);
        }

        // POST: Routes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Length,NumberOfPoints,AlreadyTravelled,DateOfCreation,LastUpdate,Approved,CreatorId")] Route route)
        {
            if (id != route.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(route);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RouteExists(route.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CreatorId"] = new SelectList(_context.Tourists, "Id", "Email", route.CreatorId);
            return View(route);
        }

        // GET: Routes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var route = await _context.Routes
                .Include(r => r.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (route == null)
            {
                return NotFound();
            }

            return View(route);
        }

        // POST: Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var route = await _context.Routes.FindAsync(id);
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RouteExists(int id)
        {
            return _context.Routes.Any(e => e.Id == id);
        }
    }
}
