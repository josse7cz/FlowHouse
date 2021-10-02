using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowHouse.Data;
using FlowHouse.Models.ViewModels;
using FlowHouse.Models;
using Microsoft.AspNetCore.Authorization;

namespace FlowHouse.Controllers
{
   // [Authorize]
    public class ProdavaciController : Controller
    {
        private readonly SkladContext _context;

        public ProdavaciController(SkladContext context)
        {
            _context = context;
        }

        // GET: Prodavaci
        public async Task<IActionResult> Index(int? id, int? polozkaID)
        {
            var viewModel = new InstructorIndexData();
            viewModel.Prodavaci = await _context.Prodavaci
                  .Include(i => i.Pobocka)
                  .Include(i => i.ProdejZadani)
                    .ThenInclude(i => i.Polozka)
                        .ThenInclude(i => i.Nakupy)
                            .ThenInclude(i => i.Zakaznik)
                  .Include(i => i.ProdejZadani)
                    .ThenInclude(i => i.Polozka)
                        .ThenInclude(i => i.Oddeleni)
                  .AsNoTracking()
                  .OrderBy(i => i.Prijmeni)
                  .ToListAsync();

            if (id != null)
            {
                ViewData["ProdavacID"] = id.Value;
                Prodavac prodavac = viewModel.Prodavaci.Where(
                    i => i.ProdavacID == id.Value).Single();
                viewModel.Polozky = prodavac.ProdejZadani.Select(s => s.Polozka);
            }

            if (polozkaID != null)
            {
                ViewData["PolozkaID"] = polozkaID.Value;
                viewModel.Nakupy = viewModel.Polozky.Where(
                    x => x.PolozkaID == polozkaID).Single().Nakupy;
            }

            return View(viewModel);
        }

        // GET: Prodavaci/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodavac = await _context.Prodavaci
                .FirstOrDefaultAsync(m => m.ProdavacID == id);
            if (prodavac == null)
            {
                return NotFound();
            }

            return View(prodavac);
        }

        // GET: Prodavaci/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prodavaci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdavacID,Prijmeni,Jmeno,HireDate")] Prodavac prodavac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prodavac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prodavac);
        }

        // GET: Prodavaci/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodavac = await _context.Prodavaci.FindAsync(id);
            if (prodavac == null)
            {
                return NotFound();
            }
            return View(prodavac);
        }

        // POST: Prodavaci/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdavacID,Prijmeni,Jmeno,HireDate")] Prodavac prodavac)
        {
            if (id != prodavac.ProdavacID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prodavac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdavacExists(prodavac.ProdavacID))
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
            return View(prodavac);
        }

        // GET: Prodavaci/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prodavac = await _context.Prodavaci
                .FirstOrDefaultAsync(m => m.ProdavacID == id);
            if (prodavac == null)
            {
                return NotFound();
            }

            return View(prodavac);
        }

        // POST: Prodavaci/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prodavac = await _context.Prodavaci.FindAsync(id);
            _context.Prodavaci.Remove(prodavac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdavacExists(int id)
        {
            return _context.Prodavaci.Any(e => e.ProdavacID == id);
        }
    }
}
