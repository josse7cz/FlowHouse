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
    public class DepartmentsController : Controller
    {
        private readonly SkladContext _context;

        public DepartmentsController(SkladContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var skladContext = _context.Departments.Include(o => o.Administrator);
            return View(await skladContext.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oddeleni = await _context.Departments
                .Include(o => o.Administrator)
                .FirstOrDefaultAsync(m => m.OddeleniID == id);
            if (oddeleni == null)
            {
                return NotFound();
            }

            return View(oddeleni);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OddeleniID,Jmeno,Budget,StartDate,ProdavacID")] Oddeleni oddeleni)
        {
            if (ModelState.IsValid)
            {
                _context.Add(oddeleni);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno", oddeleni.ProdavacID);
            return View(oddeleni);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oddeleni = await _context.Departments.FindAsync(id);
            if (oddeleni == null)
            {
                return NotFound();
            }
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno", oddeleni.ProdavacID);
            return View(oddeleni);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OddeleniID,Jmeno,Budget,StartDate,ProdavacID")] Oddeleni oddeleni)
        {
            if (id != oddeleni.OddeleniID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(oddeleni);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OddeleniExists(oddeleni.OddeleniID))
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
            ViewData["ProdavacID"] = new SelectList(_context.Prodavaci, "ProdavacID", "Jmeno", oddeleni.ProdavacID);
            return View(oddeleni);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oddeleni = await _context.Departments
                .Include(o => o.Administrator)
                .FirstOrDefaultAsync(m => m.OddeleniID == id);
            if (oddeleni == null)
            {
                return NotFound();
            }

            return View(oddeleni);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oddeleni = await _context.Departments.FindAsync(id);
            _context.Departments.Remove(oddeleni);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OddeleniExists(int id)
        {
            return _context.Departments.Any(e => e.OddeleniID == id);
        }
    }
}
