﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GOTurystyka.Models;

namespace GOTurystyka.Controllers
{
    public class PointsController : Controller
    {
        private readonly GOTurystykaContext _context;

        public PointsController(GOTurystykaContext context)
        {
            _context = context;
        }

        // GET: Points
        public async Task<IActionResult> Index()
        {
            var gOTurystykaContext = _context.Points.Include(p => p.Admin);
            return View(await gOTurystykaContext.ToListAsync());
        }

        // GET: Points/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var point = await _context.Points
                .Include(p => p.Admin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (point == null)
            {
                return NotFound();
            }

            return View(point);
        }

        // GET: Points/Create
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Email");
            return View();
        }

        // POST: Points/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Longitude,Latitude,Height,AdminId")] Point point)
        {
            if (ModelState.IsValid)
            {
                _context.Add(point);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Email", point.AdminId);
            return View(point);
        }

        // GET: Points/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var point = await _context.Points.FindAsync(id);
            if (point == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Email", point.AdminId);
            return View(point);
        }

        // POST: Points/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Longitude,Latitude,Height,AdminId")] Point point)
        {
            if (id != point.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(point);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PointExists(point.Id))
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
            ViewData["AdminId"] = new SelectList(_context.Admins, "Id", "Email", point.AdminId);
            return View(point);
        }

        // GET: Points/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var point = await _context.Points
                .Include(p => p.Admin)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (point == null)
            {
                return NotFound();
            }

            return View(point);
        }

        // POST: Points/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var point = await _context.Points.FindAsync(id);
            _context.Points.Remove(point);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PointExists(int id)
        {
            return _context.Points.Any(e => e.Id == id);
        }
    }
}
