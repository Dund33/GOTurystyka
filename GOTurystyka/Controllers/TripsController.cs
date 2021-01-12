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
    public class TripsController : Controller
    {
        private readonly GOTurystykaContext _context;
        //TODO: remove hardcoded UserId
        private const int UserId = 1;

        public TripsController(GOTurystykaContext context)
        {
            _context = context;
        }

        // GET: Trips
        public async Task<IActionResult> Index()
        {
            var decoratedTrips = _context.Trips
                .Include(t => t.Route)
                .Include(t => t.Tourist)
                .Select(trip => new TripDecorator
                {
                    Trip = trip,
                    Joined = _context.UsersInTrips
                        .Any(uit => uit.UserId == UserId)
                });

            return View(await decoratedTrips.ToListAsync());
        }

        //GET: Trips/Join/5
        public async Task<IActionResult> Join(int? id)
        {
            if (id == null)
                return NotFound();
            var alreadyJoined = await AlreadyJoined(id, UserId);

            if (alreadyJoined)
                return View("Index");

            await JoinTrip(id.Value, UserId);

            return View("Joined");
        }

        public async Task<IActionResult> Leave(int? id)
        {
            if (id == null)
                return NotFound();

            var alreadyLeft = !await _context.UsersInTrips
                .Where(uit => uit.TripId == id)
                .Where(uit => uit.UserId == UserId)
                .AnyAsync();

            if (alreadyLeft)
                return RedirectToAction("Index");


            _context.UsersInTrips.Remove(new UsersInTrip
            {
                TripId = id.Value,
                UserId = UserId
            });

            await _context.SaveChangesAsync();

            return View("Left");
        }

        // GET: Trips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(t => t.Route)
                .Include(t => t.Tourist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // GET: Trips/Create
        public IActionResult Create()
        {
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name");
            ViewData["TouristId"] = new SelectList(_context.Tourists, "Id", "Email");
            return View();
        }

        // POST: Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Date,Ended,Confirmed,RouteId,TouristId")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trip);
                await _context.SaveChangesAsync();
                await JoinTrip(trip.Id, UserId);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", trip.RouteId);
            ViewData["TouristId"] = new SelectList(_context.Tourists, "Id", "Email", trip.TouristId);
            return View(trip);
        }

        // GET: Trips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips.FindAsync(id);
            if (trip == null)
            {
                return NotFound();
            }
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", trip.RouteId);
            ViewData["TouristId"] = new SelectList(_context.Tourists, "Id", "Email", trip.TouristId);
            return View(trip);
        }

        // POST: Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Date,Ended,Confirmed,RouteId,TouristId")] Trip trip)
        {
            if (id != trip.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripExists(trip.Id))
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
            ViewData["RouteId"] = new SelectList(_context.Routes, "Id", "Name", trip.RouteId);
            ViewData["TouristId"] = new SelectList(_context.Tourists, "Id", "Email", trip.TouristId);
            return View(trip);
        }

        // GET: Trips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trip = await _context.Trips
                .Include(t => t.Route)
                .Include(t => t.Tourist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trip == null)
            {
                return NotFound();
            }

            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trip = await _context.Trips.FindAsync(id);
            _context.Trips.Remove(trip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TripExists(int id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }

        private async Task<bool> AlreadyJoined(int? id, int touristId)
        {
            var alreadyJoined = await _context.UsersInTrips
                .Where(uit => uit.TripId == id)
                .Where(uit => uit.UserId == UserId)
                .AnyAsync();
            return alreadyJoined;
        }

        private async Task JoinTrip(int id, int userId)
        {
            await _context.UsersInTrips.AddAsync(new UsersInTrip
            {
                TripId = id,
                UserId = userId
            });
            await _context.SaveChangesAsync();
        }
    }
}
