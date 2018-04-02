using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApproxiMATEwebApi.Controllers
{
    [Authorize]
    public class ZoneCitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZoneCitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ZoneCities
        public async Task<IActionResult> Index()
        {
            return View(await _context.ZoneCities.ToListAsync());
        }

        // GET: ZoneCities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneCity = await _context.ZoneCities
                .SingleOrDefaultAsync(m => m.CityId == id);
            if (zoneCity == null)
            {
                return NotFound();
            }

            return View(zoneCity);
        }

        // GET: ZoneCities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZoneCities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CityId,Description")] ZoneCity zoneCity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zoneCity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zoneCity);
        }

        // GET: ZoneCities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneCity = await _context.ZoneCities.SingleOrDefaultAsync(m => m.CityId == id);
            if (zoneCity == null)
            {
                return NotFound();
            }
            return View(zoneCity);
        }

        // POST: ZoneCities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CityId,Description")] ZoneCity zoneCity)
        {
            if (id != zoneCity.CityId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zoneCity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZoneCityExists(zoneCity.CityId))
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
            return View(zoneCity);
        }

        // GET: ZoneCities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneCity = await _context.ZoneCities
                .SingleOrDefaultAsync(m => m.CityId == id);
            if (zoneCity == null)
            {
                return NotFound();
            }

            return View(zoneCity);
        }

        // POST: ZoneCities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zoneCity = await _context.ZoneCities.SingleOrDefaultAsync(m => m.CityId == id);
            _context.ZoneCities.Remove(zoneCity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZoneCityExists(int id)
        {
            return _context.ZoneCities.Any(e => e.CityId == id);
        }
    }
}
