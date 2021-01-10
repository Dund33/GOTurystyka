using System.Linq;
using System.Threading.Tasks;
using GOTurystyka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GOTurystyka.Controllers
{
    public class ForemenController : Controller
    {
        private readonly GOTurystykaContext _context;

        public ForemenController(GOTurystykaContext context)
        {
            _context = context;
        }

        // GET: Foremen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Foremen.ToListAsync());
        }

        // GET: Foremen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var foreman = await _context.Foremen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foreman == null) return NotFound();

            return View(foreman);
        }

        // GET: Foremen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foremen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Name,LastName,DateOfBirth,PhoneNumber,LoggedIn,Email,Login,Password,Points,Active")]
            Foreman foreman)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foreman);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(foreman);
        }

        // GET: Foremen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var foreman = await _context.Foremen.FindAsync(id);
            if (foreman == null) return NotFound();
            return View(foreman);
        }

        // POST: Foremen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Name,LastName,DateOfBirth,PhoneNumber,LoggedIn,Email,Login,Password,Points,Active")]
            Foreman foreman)
        {
            if (id != foreman.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foreman);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForemanExists(foreman.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(foreman);
        }

        // GET: Foremen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var foreman = await _context.Foremen
                .FirstOrDefaultAsync(m => m.Id == id);
            if (foreman == null) return NotFound();

            return View(foreman);
        }

        // POST: Foremen/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foreman = await _context.Foremen.FindAsync(id);
            _context.Foremen.Remove(foreman);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForemanExists(int id)
        {
            return _context.Foremen.Any(e => e.Id == id);
        }
    }
}