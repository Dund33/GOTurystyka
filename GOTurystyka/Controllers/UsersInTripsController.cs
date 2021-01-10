using System.Linq;
using System.Threading.Tasks;
using GOTurystyka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GOTurystyka.Controllers
{
    public class UsersInTripsController : Controller
    {
        private readonly GOTurystykaContext _context;

        public UsersInTripsController(GOTurystykaContext context)
        {
            _context = context;
        }

        // GET: UsersInTrips
        public async Task<IActionResult> Index()
        {
            var gOTurystykaContext = _context.UsersInTrips.Include(u => u.Trip).Include(u => u.User);
            return View(await gOTurystykaContext.ToListAsync());
        }

        // GET: UsersInTrips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var usersInTrip = await _context.UsersInTrips
                .Include(u => u.Trip)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (usersInTrip == null) return NotFound();

            return View(usersInTrip);
        }

        // GET: UsersInTrips/Create
        public IActionResult Create()
        {
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: UsersInTrips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,TripId")] UsersInTrip usersInTrip)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usersInTrip);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Name", usersInTrip.TripId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", usersInTrip.UserId);
            return View(usersInTrip);
        }

        // GET: UsersInTrips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var usersInTrip = await _context.UsersInTrips.FindAsync(id);
            if (usersInTrip == null) return NotFound();
            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Name", usersInTrip.TripId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", usersInTrip.UserId);
            return View(usersInTrip);
        }

        // POST: UsersInTrips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,TripId")] UsersInTrip usersInTrip)
        {
            if (id != usersInTrip.UserId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usersInTrip);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersInTripExists(usersInTrip.UserId))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["TripId"] = new SelectList(_context.Trips, "Id", "Name", usersInTrip.TripId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", usersInTrip.UserId);
            return View(usersInTrip);
        }

        // GET: UsersInTrips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var usersInTrip = await _context.UsersInTrips
                .Include(u => u.Trip)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (usersInTrip == null) return NotFound();

            return View(usersInTrip);
        }

        // POST: UsersInTrips/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usersInTrip = await _context.UsersInTrips.FindAsync(id);
            _context.UsersInTrips.Remove(usersInTrip);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersInTripExists(int id)
        {
            return _context.UsersInTrips.Any(e => e.UserId == id);
        }
    }
}