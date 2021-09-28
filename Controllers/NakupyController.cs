using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowHouse.Data;
using FlowHouse.Models;

namespace FlowHouse.Controllers
{
    public class NakupyController : Controller
    {
        private readonly SkladContext _context;

        public NakupyController(SkladContext context)
        {
            _context = context;
        }

        // GET: Nakupy
        public async Task<IActionResult> Index()
        {
            var skladContext = _context.Nakupy.Include(n => n.Polozka).Include(n => n.Prodavac).Include(n => n.Zakaznik);
            return View(await skladContext.ToListAsync());
        }

        // GET: Nakupy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nakup = await _context.Nakupy
                .Include(n => n.Polozka)
                .Include(n => n.Prodavac)
                .Include(n => n.Zakaznik)
                .FirstOrDefaultAsync(m => m.NakupID == id);
            if (nakup == null)
            {
                return NotFound();
            }

            return View(nakup);
        }

        // GET: Nakupy/Create
        public IActionResult Create()
        {
            ViewData["PolozkaID"] = new SelectList(_context.Polozky, "PolozkaID", "Nazev");
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno");
            ViewData["ZakaznikID"] = new SelectList(_context.Zakaznici, "ZakaznikID", "Jmeno");
            return View();
        }

        // POST: Nakupy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NakupID,PolozkaID,ZakaznikID,ProdavacID,Cena")] Nakup nakup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nakup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PolozkaID"] = new SelectList(_context.Polozky, "PolozkaID", "Nazev", nakup.PolozkaID);
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno", nakup.ProdavacID);
            ViewData["ZakaznikID"] = new SelectList(_context.Zakaznici, "ZakaznikID", "Jmeno", nakup.ZakaznikID);
            return View(nakup);
        }

        // GET: Nakupy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nakup = await _context.Nakupy.FindAsync(id);
            if (nakup == null)
            {
                return NotFound();
            }
            ViewData["PolozkaID"] = new SelectList(_context.Polozky, "PolozkaID", "Nazev", nakup.PolozkaID);
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno", nakup.ProdavacID);
            ViewData["ZakaznikID"] = new SelectList(_context.Zakaznici, "ZakaznikID", "Jmeno", nakup.ZakaznikID);
            return View(nakup);
        }

        // POST: Nakupy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NakupID,PolozkaID,ZakaznikID,ProdavacID,Cena")] Nakup nakup)
        {
            if (id != nakup.NakupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nakup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NakupExists(nakup.NakupID))
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
            ViewData["PolozkaID"] = new SelectList(_context.Polozky, "PolozkaID", "Nazev", nakup.PolozkaID);
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno", nakup.ProdavacID);
            ViewData["ZakaznikID"] = new SelectList(_context.Zakaznici, "ZakaznikID", "Jmeno", nakup.ZakaznikID);
            return View(nakup);
        }

        // GET: Nakupy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nakup = await _context.Nakupy
                .Include(n => n.Polozka)
                .Include(n => n.Prodavac)
                .Include(n => n.Zakaznik)
                .FirstOrDefaultAsync(m => m.NakupID == id);
            if (nakup == null)
            {
                return NotFound();
            }

            return View(nakup);
        }

        // POST: Nakupy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nakup = await _context.Nakupy.FindAsync(id);
            _context.Nakupy.Remove(nakup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NakupExists(int id)
        {
            return _context.Nakupy.Any(e => e.NakupID == id);
        }
    }
}
