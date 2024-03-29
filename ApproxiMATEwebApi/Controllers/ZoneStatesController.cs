﻿using System;
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
    [Route("api/ZoneStates")]
    [Authorize]
    public class ZoneStatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZoneStatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ZoneStates
        [HttpGet]
        public IEnumerable<ZoneState> GetZoneStates()
        {
            return _context.ZoneStates;
        }

        // GET: api/ZoneStates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetZoneState([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneState = await _context.ZoneStates.SingleOrDefaultAsync(m => m.StateId == id);

            if (zoneState == null)
            {
                return NotFound();
            }

            return Ok(zoneState);
        }

        // PUT: api/ZoneStates/5
        [HttpPut("{id}")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> PutZoneState([FromRoute] int id, [FromBody] ZoneState zoneState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zoneState.StateId)
            {
                return BadRequest();
            }

            _context.Entry(zoneState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneStateExists(id))
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

        // POST: api/ZoneStates
        [HttpPost]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> PostZoneState([FromBody] ZoneState zoneState)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ZoneStates.Add(zoneState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetZoneState", new { id = zoneState.StateId }, zoneState);
        }

        // DELETE: api/ZoneStates/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "AdministratorPolicy")]
        public async Task<IActionResult> DeleteZoneState([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var zoneState = await _context.ZoneStates.SingleOrDefaultAsync(m => m.StateId == id);
            if (zoneState == null)
            {
                return NotFound();
            }

            _context.ZoneStates.Remove(zoneState);
            await _context.SaveChangesAsync();

            return Ok(zoneState);
        }

        private bool ZoneStateExists(int id)
        {
            return _context.ZoneStates.Any(e => e.StateId == id);
        }
    }
}