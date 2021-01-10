using System.Linq;
using System.Threading.Tasks;
using GOTurystyka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GOTurystyka.Controllers
{
    public class SegmentsController : Controller
    {
        private readonly GOTurystykaContext _context;

        public SegmentsController(GOTurystykaContext context)
        {
            _context = context;
        }

        // GET: Segments
        public async Task<IActionResult> Index()
        {
            var gOTurystykaContext = _context.Segments.Include(s => s.Foreman);
            return View(await gOTurystykaContext.ToListAsync());
        }

        // GET: Segments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var segment = await _context.Segments
                .Include(s => s.Foreman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (segment == null) return NotFound();

            return View(segment);
        }

        // GET: Segments/Create
        public IActionResult Create()
        {
            ViewData["ForemanId"] = new SelectList(_context.Foremen, "Id", "Email");
            return View();
        }

        // POST: Segments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Points,Length,HasPoints,PointsDir1,PointsDir2,ForemanId,LicenseForId")]
            Segment segment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(segment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ForemanId"] = new SelectList(_context.Foremen, "Id", "Email", segment.ForemanId);
            return View(segment);
        }

        // GET: Segments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var segment = await _context.Segments.FindAsync(id);
            if (segment == null) return NotFound();
            ViewData["ForemanId"] = new SelectList(_context.Foremen, "Id", "Email", segment.ForemanId);
            return View(segment);
        }

        // POST: Segments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Points,Length,HasPoints,PointsDir1,PointsDir2,ForemanId,LicenseForId")]
            Segment segment)
        {
            if (id != segment.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(segment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SegmentExists(segment.Id))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ForemanId"] = new SelectList(_context.Foremen, "Id", "Email", segment.ForemanId);
            return View(segment);
        }

        // GET: Segments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var segment = await _context.Segments
                .Include(s => s.Foreman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (segment == null) return NotFound();

            return View(segment);
        }

        // POST: Segments/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var segment = await _context.Segments.FindAsync(id);
            _context.Segments.Remove(segment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SegmentExists(int id)
        {
            return _context.Segments.Any(e => e.Id == id);
        }
    }
}