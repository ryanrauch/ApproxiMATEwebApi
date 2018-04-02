using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApproxiMATEwebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/LocationHistories")]
    [Authorize]
    public class LocationHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LocationHistories
        [HttpGet]
        public IEnumerable<LocationHistory> GetLocationHistories()
        {
            return _context.LocationHistories;
        }

        // GET: api/LocationHistories/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationHistory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationHistory = await _context.LocationHistories.SingleOrDefaultAsync(m => m.HistoryID == id);

            if (locationHistory == null)
            {
                return NotFound();
            }

            return Ok(locationHistory);
        }

        // PUT: api/LocationHistories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationHistory([FromRoute] int id, [FromBody] LocationHistory locationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != locationHistory.HistoryID)
            {
                return BadRequest();
            }

            _context.Entry(locationHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationHistoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LocationHistories
        [HttpPost]
        public async Task<IActionResult> PostLocationHistory([FromBody] LocationHistory locationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.LocationHistories.Add(locationHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationHistory", new { id = locationHistory.HistoryID }, locationHistory);
        }

        // DELETE: api/LocationHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocationHistory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var locationHistory = await _context.LocationHistories.SingleOrDefaultAsync(m => m.HistoryID == id);
            if (locationHistory == null)
            {
                return NotFound();
            }

            _context.LocationHistories.Remove(locationHistory);
            await _context.SaveChangesAsync();

            return Ok(locationHistory);
        }

        private bool LocationHistoryExists(int id)
        {
            return _context.LocationHistories.Any(e => e.HistoryID == id);
        }
    }
}