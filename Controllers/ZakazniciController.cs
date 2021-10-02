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

namespace FlowHouse
{
   // [Authorize]
    public class ZakazniciController : Controller
    {
        private readonly SkladContext _context;

        public ZakazniciController(SkladContext context)
        {
            _context = context;
        }

        // GET: Zakaznici
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {
            ViewData["JmenoSortParm"] = String.IsNullOrEmpty(sortOrder) ? "jmeno_desc" : "";
            ViewData["DatumSortParm"] = sortOrder == "Datum" ? "datum_desc" : "Datum";
            ViewData["PrijmeniSortParm"] = sortOrder == "Prijmeni" ? "prijmeni_desc" : "Prijmeni";
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

            var zakaznici = from s in _context.Zakaznici
                            select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                zakaznici = zakaznici.Where(s => s.Prijmeni.Contains(searchString)
                                       || s.Jmeno.Contains(searchString));
            }
            switch (sortOrder)
            {

                case "jmeno_desc":
                    zakaznici = zakaznici.OrderByDescending(s => s.Jmeno);
                    break;
                case "prijmeni_desc":
                    zakaznici = zakaznici.OrderByDescending(s => s.Prijmeni);
                    break;
                case "Datum":
                    zakaznici = zakaznici.OrderBy(s => s.DatumRegistrace);
                    break;

                default:
                    zakaznici = zakaznici.OrderBy(s => s.Prijmeni);
                    break;
            }
            int pageSize = 5;
            return View(await PaginatedList<Zakaznik>.CreateAsync(zakaznici.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Zakaznici/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var zakaznik = await _context.Zakaznici
            //    .FirstOrDefaultAsync(m => m.ZakaznikID == id);


            var zakaznik = await _context.Zakaznici
        .Include(s => s.Nakupy)
            .ThenInclude(e => e.Polozka)
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.ZakaznikID == id);



            if (zakaznik == null)
            {
                return NotFound();
            }

            return View(zakaznik);
        }

        // GET: Zakaznici/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zakaznici/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Jmeno,Prijmeni,DatumRegistrace")] Zakaznik zakaznik)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(zakaznik);
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
            return View(zakaznik);
        }

        // GET: Zakaznici/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zakaznik = await _context.Zakaznici.FindAsync(id);
            if (zakaznik == null)
            {
                return NotFound();
            }
            return View(zakaznik);
        }

        // POST: Zakaznici/Edit/5
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
            var zakaznikToUpdate = await _context.Zakaznici.FirstOrDefaultAsync(s => s.ZakaznikID == id);
            if (await TryUpdateModelAsync<Zakaznik>(
                zakaznikToUpdate,
                "",
                s => s.Jmeno, s => s.Prijmeni, s => s.DatumRegistrace))
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
            return View(zakaznikToUpdate);
        }

        // GET: Zakaznici/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zakaznik = await _context.Zakaznici.AsNoTracking()
                .FirstOrDefaultAsync(m => m.ZakaznikID == id);
            if (zakaznik == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }
            return View(zakaznik);
        }

        // POST: Zakaznici/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zakaznik = await _context.Zakaznici.FindAsync(id);
            if (zakaznik == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {

                _context.Zakaznici.Remove(zakaznik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool ZakaznikExists(int id)
        {
            return _context.Zakaznici.Any(e => e.ZakaznikID == id);
        }
    }
}
