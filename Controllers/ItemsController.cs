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
   // [Authorize]
    public class ItemsController : Controller
    {
        private readonly SkladContext _context;

        public ItemsController(SkladContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(
           string sortOrder,
           string currentFilter,
           string searchString,
           int? pageNumber)

        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["OddeleniSortParm"] = sortOrder == "Oddeleni" ? "oddeleni_desc" : "Oddeleni";
            ViewData["CenaSortParm"] = sortOrder == "Cena" ? "cena_desc" : "Cena";
            ViewData["PocetSortParm"] = sortOrder == "Pocet" ? "pocet_desc" : "Pocet";
            ViewData["CurrentSort"] = sortOrder;

            //var skladContext = _context.Polozky.Include(p => p.Oddeleni);
            var skladContext = from s in _context.Polozky.Include(s => s.Oddeleni)
                               select s;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                skladContext = skladContext.Where(s => s.Nazev.Contains(searchString));
            }
                        

            switch (sortOrder)
            {
                case "name_desc":
                    skladContext = skladContext.OrderByDescending(s => s.Nazev);
                    break;
               case "date_desc":
                    skladContext = skladContext.OrderByDescending(s => s.DatumNaskladneni);
                    break;
                case "cena_desc":
                    skladContext = skladContext.OrderByDescending(s => s.Cena);
                    break;

                case "pocet_desc":
                    skladContext = skladContext.OrderByDescending(s => s.Pocet);
                    break;
                default:
                    skladContext = skladContext.OrderBy(s => s.Oddeleni);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Polozka>.CreateAsync(skladContext.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polozka = await _context.Polozky
                .Include(p => p.Oddeleni)
                .FirstOrDefaultAsync(m => m.PolozkaID == id);
            if (polozka == null)
            {
                return NotFound();
            }

            return View(polozka);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["OddeleniID"] = new SelectList(_context.Departments, "OddeleniID", "OddeleniID");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PolozkaID,Nazev,Cena,Pocet,DatumNaskladneni,OddeleniID")] Polozka polozka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(polozka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OddeleniID"] = new SelectList(_context.Departments, "OddeleniID", "OddeleniID", polozka.OddeleniID);
            return View(polozka);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polozka = await _context.Polozky.FindAsync(id);
            if (polozka == null)
            {
                return NotFound();
            }
            ViewData["OddeleniID"] = new SelectList(_context.Departments, "OddeleniID", "OddeleniID", polozka.OddeleniID);
            return View(polozka);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PolozkaID,Nazev,Cena,Pocet,DatumNaskladneni,OddeleniID")] Polozka polozka)
        {
            if (id != polozka.PolozkaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(polozka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolozkaExists(polozka.PolozkaID))
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
            ViewData["OddeleniID"] = new SelectList(_context.Departments, "OddeleniID", "OddeleniID", polozka.OddeleniID);
            return View(polozka);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polozka = await _context.Polozky
                .Include(p => p.Oddeleni)
                .FirstOrDefaultAsync(m => m.PolozkaID == id);
            if (polozka == null)
            {
                return NotFound();
            }

            return View(polozka);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var polozka = await _context.Polozky.FindAsync(id);
            _context.Polozky.Remove(polozka);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolozkaExists(int id)
        {
            return _context.Polozky.Any(e => e.PolozkaID == id);
        }
    }
}
