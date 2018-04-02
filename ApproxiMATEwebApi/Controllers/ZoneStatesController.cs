using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;

namespace ApproxiMATEwebApi.Controllers
{
    public class ZoneStatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZoneStatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ZoneStates
        public async Task<IActionResult> Index()
        {
            return View(await _context.ZoneStates.ToListAsync());
        }

        // GET: ZoneStates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneState = await _context.ZoneStates
                .SingleOrDefaultAsync(m => m.StateId == id);
            if (zoneState == null)
            {
                return NotFound();
            }

            return View(zoneState);
        }

        // GET: ZoneStates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ZoneStates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateId,Description,ShortDescription")] ZoneState zoneState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zoneState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(zoneState);
        }

        // GET: ZoneStates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneState = await _context.ZoneStates.SingleOrDefaultAsync(m => m.StateId == id);
            if (zoneState == null)
            {
                return NotFound();
            }
            return View(zoneState);
        }

        // POST: ZoneStates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,Description,ShortDescription")] ZoneState zoneState)
        {
            if (id != zoneState.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zoneState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZoneStateExists(zoneState.StateId))
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
            return View(zoneState);
        }

        // GET: ZoneStates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zoneState = await _context.ZoneStates
                .SingleOrDefaultAsync(m => m.StateId == id);
            if (zoneState == null)
            {
                return NotFound();
            }

            return View(zoneState);
        }

        // POST: ZoneStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zoneState = await _context.ZoneStates.SingleOrDefaultAsync(m => m.StateId == id);
            _context.ZoneStates.Remove(zoneState);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZoneStateExists(int id)
        {
            return _context.ZoneStates.Any(e => e.StateId == id);
        }
    }
}
