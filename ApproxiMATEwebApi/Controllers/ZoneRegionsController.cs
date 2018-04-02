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
    [Route("api/ZoneRegions")]
    [Authorize]
    public class ZoneRegionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZoneRegionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ZoneRegions
        [HttpGet]
        public IEnumerable<ZoneRegion> GetZoneRegions()
        {
            return _context.ZoneRegions;
        }

        // GET: api/ZoneRegions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetZoneRegion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneRegion = await _context.ZoneRegions.SingleOrDefaultAsync(m => m.RegionId == id);

            if (zoneRegion == null)
            {
                return NotFound();
            }

            return Ok(zoneRegion);
        }

        // PUT: api/ZoneRegions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZoneRegion([FromRoute] int id, [FromBody] ZoneRegion zoneRegion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zoneRegion.RegionId)
            {
                return BadRequest();
            }

            _context.Entry(zoneRegion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneRegionExists(id))
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

        // POST: api/ZoneRegions
        [HttpPost]
        public async Task<IActionResult> PostZoneRegion([FromBody] ZoneRegion zoneRegion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ZoneRegions.Add(zoneRegion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZoneRegion", new { id = zoneRegion.RegionId }, zoneRegion);
        }

        // DELETE: api/ZoneRegions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZoneRegion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneRegion = await _context.ZoneRegions.SingleOrDefaultAsync(m => m.RegionId == id);
            if (zoneRegion == null)
            {
                return NotFound();
            }

            _context.ZoneRegions.Remove(zoneRegion);
            await _context.SaveChangesAsync();

            return Ok(zoneRegion);
        }

        private bool ZoneRegionExists(int id)
        {
            return _context.ZoneRegions.Any(e => e.RegionId == id);
        }
    }
}