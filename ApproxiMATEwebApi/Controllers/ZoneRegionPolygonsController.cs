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
    [Route("api/ZoneRegionPolygons")]
    [Authorize]
    public class ZoneRegionPolygonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZoneRegionPolygonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ZoneRegionPolygons
        [HttpGet]
        public IEnumerable<ZoneRegionPolygon> GetZoneRegionPolygons()
        {
            return _context.ZoneRegionPolygons;
        }

        // GET: api/ZoneRegionPolygons/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetZoneRegionPolygon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneRegionPolygon = await _context.ZoneRegionPolygons.SingleOrDefaultAsync(m => m.Order == id);

            if (zoneRegionPolygon == null)
            {
                return NotFound();
            }

            return Ok(zoneRegionPolygon);
        }

        // PUT: api/ZoneRegionPolygons/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZoneRegionPolygon([FromRoute] int id, [FromBody] ZoneRegionPolygon zoneRegionPolygon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zoneRegionPolygon.Order)
            {
                return BadRequest();
            }

            _context.Entry(zoneRegionPolygon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneRegionPolygonExists(id))
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

        // POST: api/ZoneRegionPolygons
        [HttpPost]
        public async Task<IActionResult> PostZoneRegionPolygon([FromBody] ZoneRegionPolygon zoneRegionPolygon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ZoneRegionPolygons.Add(zoneRegionPolygon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZoneRegionPolygon", new { id = zoneRegionPolygon.Order }, zoneRegionPolygon);
        }

        // DELETE: api/ZoneRegionPolygons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZoneRegionPolygon([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneRegionPolygon = await _context.ZoneRegionPolygons.SingleOrDefaultAsync(m => m.Order == id);
            if (zoneRegionPolygon == null)
            {
                return NotFound();
            }

            _context.ZoneRegionPolygons.Remove(zoneRegionPolygon);
            await _context.SaveChangesAsync();

            return Ok(zoneRegionPolygon);
        }

        private bool ZoneRegionPolygonExists(int id)
        {
            return _context.ZoneRegionPolygons.Any(e => e.Order == id);
        }
    }
}