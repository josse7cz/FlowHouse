using FlowHouse.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FlowHouse.Data;
using FlowHouse.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FlowHouse.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SkladContext _context;

        public HomeController(ILogger<HomeController> logger, SkladContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<ActionResult> About()//vytahnuti dat z DB podle data registrace
        {
            IQueryable<EnrollmentDateGroup> data =
                from zakaznik in _context.Zakaznici
                group zakaznik by zakaznik.DatumRegistrace into dateGroup
                select new EnrollmentDateGroup()
                {
                    EnrollmentDate = dateGroup.Key,
                    ZakaznikCount = dateGroup.Count()
                   
                };
            return View(await data.AsNoTracking().ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
