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
    public class PolozkyController : Controller
    {
        private readonly SkladContext _context;

        public PolozkyController(SkladContext context)
        {
            _context = context;
        }

        // GET: Polozky
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)

        {
            ViewData["NazevSortParm"] = String.IsNullOrEmpty(sortOrder) ? "nazev_desc" : "";
            ViewData["DatumSortParm"] = sortOrder == "Datum" ? "datum_desc" : "Datum";
            ViewData["CenaSortParm"] = sortOrder == "Cena" ? "cena_desc" : "Cena";
            ViewData["PocetSortParm"] = sortOrder == "Pocet" ? "pocet_desc" : "Pocet";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var polozky = from s in _context.Polozky
                          select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                polozky = polozky.Where(s => s.Nazev.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nazev_desc":
                    polozky = polozky.OrderByDescending(s => s.Nazev);
                    break;
                case "Datum":
                    polozky = polozky.OrderBy(s => s.DatumNaskladneni);
                    break;
                case "datum_desc":
                    polozky = polozky.OrderByDescending(s => s.DatumNaskladneni);
                    break;
                case "cena_desc":
                    polozky = polozky.OrderByDescending(s => s.Cena);
                    break;

                case "pocet_desc":
                    polozky = polozky.OrderByDescending(s => s.Pocet);
                    break;
                default:
                    polozky = polozky.OrderBy(s => s.Nazev);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Polozka>.CreateAsync(polozky.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Polozky/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var polozka = await _context.Polozky
            //    .FirstOrDefaultAsync(m => m.PolozkaID == id);

            var polozka = await _context.Polozky
                .AsNoTracking()
        .FirstOrDefaultAsync(m => m.PolozkaID == id);

            //predelat na detail o oddeleni a zodpovedny zamestanance
            //    var student = await _context.polozky
            //.Include(s => s.Enrollments)
            //    .ThenInclude(e => e.Course)
            //.AsNoTracking()
            //.FirstOrDefaultAsync(m => m.ID == id);


            if (polozka == null)
            {
                return NotFound();
            }

            return View(polozka);
        }

        // GET: Polozky/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Polozky/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nazev,Cena,Pocet,DatumNaskladneni")] Polozka polozka)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(polozka);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Nemozne ulozit zmeny. " +
                    "Zkuste znovu, a pokud problém přetrvává " +
                    "požádejte správce systému.");
            }
            return View(polozka);
        }

        // GET: Polozky/Edit/5
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
            return View(polozka);
        }

        // POST: Polozky/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var polozkaToUpdate = await _context.Polozky.FirstOrDefaultAsync(s => s.PolozkaID == id);
            if (await TryUpdateModelAsync<Polozka>(
                polozkaToUpdate,
                "",
                s => s.Nazev, s => s.Pocet, s => s.DatumNaskladneni, s => s.Cena))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(polozkaToUpdate);
        }

        // GET: Polozky/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var polozka = await _context.Polozky.AsNoTracking()
                .FirstOrDefaultAsync(m => m.PolozkaID == id);
            if (polozka == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(polozka);
        }

        // POST: Polozky/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var polozka = await _context.Polozky.FindAsync(id);
            if (polozka == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Polozky.Remove(polozka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool PolozkaExists(int id)
        {
            return _context.Polozky.Any(e => e.PolozkaID == id);
        }
    }
}
