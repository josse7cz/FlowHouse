using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowHouse.Data;
using FlowHouse.Models;
using Microsoft.AspNetCore.Authorization;

namespace FlowHouse.Controllers
{
    //[Authorize]
    public class PobockyController : Controller
    {
        private readonly SkladContext _context;

        public PobockyController(SkladContext context)
        {
            _context = context;
        }

        // GET: Pobocky
        public async Task<IActionResult> Index()
        {
            var skladContext = _context.Pobocky.Include(p => p.Prodavac);
            return View(await skladContext.ToListAsync());
        }

        // GET: Pobocky/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pobocka = await _context.Pobocky
                .Include(p => p.Prodavac)
                .FirstOrDefaultAsync(m => m.ProdavacID == id);
            if (pobocka == null)
            {
                return NotFound();
            }

            return View(pobocka);
        }

        // GET: Pobocky/Create
        public IActionResult Create()
        {
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno");
            return View();
        }

        // POST: Pobocky/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdavacID,Location")] Pobocka pobocka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pobocka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno", pobocka.ProdavacID);
            return View(pobocka);
        }

        // GET: Pobocky/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pobocka = await _context.Pobocky.FindAsync(id);
            if (pobocka == null)
            {
                return NotFound();
            }
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno", pobocka.ProdavacID);
            return View(pobocka);
        }

        // POST: Pobocky/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdavacID,Location")] Pobocka pobocka)
        {
            if (id != pobocka.ProdavacID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pobocka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PobockaExists(pobocka.ProdavacID))
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
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno", pobocka.ProdavacID);
            return View(pobocka);
        }

        // GET: Pobocky/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pobocka = await _context.Pobocky
                .Include(p => p.Prodavac)
                .FirstOrDefaultAsync(m => m.ProdavacID == id);
            if (pobocka == null)
            {
                return NotFound();
            }

            return View(pobocka);
        }

        // POST: Pobocky/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pobocka = await _context.Pobocky.FindAsync(id);
            _context.Pobocky.Remove(pobocka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PobockaExists(int id)
        {
            return _context.Pobocky.Any(e => e.ProdavacID == id);
        }
    }
}
