using GOTurystyka.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GOTurystyka.Controllers
{
    public class SegmentsController : Controller
    {
        private readonly GOTurystykaContext _context;

        //Todo: remove hardcoded foreman Id
        private const int ForemanId = 1;

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

        // GET: FinishedSegments
        public async Task<IActionResult> FinishedSegments()
        {

            var segment = await _context.Routes
                .Where(r => r.WaitingForApproval)
                .ToListAsync();
            ViewBag.CallingMethod = "FinishedRoutes";
            return View("Index", segment);
        }
        // GET: Finish
        public async Task<IActionResult> Finish(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Routes.
                Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            segment.AlreadyTravelled = true;
            segment.WaitingForApproval = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Approve 
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();

            if (segment.Approved == true)
            {
                return Ok("Segment is already approved!");
            }

            segment.Approved = true;
            return View("Index");
        }

        // GET: Segments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments
                .Include(s => s.Foreman)
                .FirstOrDefaultAsync(m => m.Id == id);
            var points = await _context.PointsInSegments
                .Where(p => p.Segment == segment)
                .Include(p => p.Point)
                .Select(p => p.Point)
                .ToListAsync();

            if (segment == null)
            {
                return NotFound();
            }

            return View((Segment: segment, Points: points));
        }

        // GET: Segments/Create
        public IActionResult Create()
        {
            ViewData["ForemanId"] = new SelectList(_context.Foremen, "Id", "Email");
            return View();
        }

        // POST: Segments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Points,Length,HasPoints,PointsDir1,PointsDir2,ForemanId,LicenseForId,CreatorId")] Segment segment)
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
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments.FindAsync(id);
            if (segment == null)
            {
                return NotFound();
            }
            ViewData["ForemanId"] = new SelectList(_context.Foremen, "Id", "Email", segment.ForemanId);
            ViewData["CreatorId"] = new SelectList(_context.Tourists, "Id", "LastName", segment.CreatorId);
            return View(segment);
        }

        // POST: Segments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Points,Length,HasPoints,PointsDir1,PointsDir2,ForemanId,LicenseForId,CreatorId,AlreadyTravelled")] Segment segment)
        {
            if (id != segment.Id)
            {
                return NotFound();
            }

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
            ViewData["ForemanId"] = new SelectList(_context.Foremen, "Id", "Email", segment.ForemanId);
            ViewData["CreatorId"] = new SelectList(_context.Tourists, "Id", "LastName", segment.CreatorId);
            return View(segment);
        }

        // GET: Segments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var segment = await _context.Segments
                .Include(s => s.Foreman)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (segment == null)
            {
                return NotFound();
            }

            return View(segment);
        }

        // POST: Segments/Delete/5
        [HttpPost, ActionName("Delete")]
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
