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
    [Route("api/ZoneCities")]
    [Authorize]
    public class ZoneCitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZoneCitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ZoneCities
        [HttpGet]
        public IEnumerable<ZoneCity> GetZoneCities()
        {
            return _context.ZoneCities;
        }

        // GET: api/ZoneCities/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetZoneCity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneCity = await _context.ZoneCities.SingleOrDefaultAsync(m => m.CityId == id);

            if (zoneCity == null)
            {
                return NotFound();
            }

            return Ok(zoneCity);
        }

        // PUT: api/ZoneCities/5
        [HttpPut("{id}")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> PutZoneCity([FromRoute] int id, [FromBody] ZoneCity zoneCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zoneCity.CityId)
            {
                return BadRequest();
            }

            _context.Entry(zoneCity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneCityExists(id))
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

        // POST: api/ZoneCities
        [HttpPost]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> PostZoneCity([FromBody] ZoneCity zoneCity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ZoneCities.Add(zoneCity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZoneCity", new { id = zoneCity.CityId }, zoneCity);
        }

        // DELETE: api/ZoneCities/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> DeleteZoneCity([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneCity = await _context.ZoneCities.SingleOrDefaultAsync(m => m.CityId == id);
            if (zoneCity == null)
            {
                return NotFound();
            }

            _context.ZoneCities.Remove(zoneCity);
            await _context.SaveChangesAsync();

            return Ok(zoneCity);
        }

        private bool ZoneCityExists(int id)
        {
            return _context.ZoneCities.Any(e => e.CityId == id);
        }
    }
}