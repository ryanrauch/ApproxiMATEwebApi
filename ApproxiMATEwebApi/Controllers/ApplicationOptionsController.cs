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
    [Route("api/ApplicationOptions")]
    [Authorize]
    public class ApplicationOptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicationOptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApplicationOptions
        [HttpGet]
        public IEnumerable<ApplicationOption> GetApplicationOptions()
        {
            return _context.ApplicationOptions;
        }

        // GET: api/ApplicationOptions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationOption([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationOption = await _context.ApplicationOptions.SingleOrDefaultAsync(m => m.OptionsId == id);

            if (applicationOption == null)
            {
                return NotFound();
            }

            return Ok(applicationOption);
        }

        /*
        // PUT: api/ApplicationOptions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationOption([FromRoute] int id, [FromBody] ApplicationOption applicationOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationOption.OptionsId)
            {
                return BadRequest();
            }

            _context.Entry(applicationOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationOptionExists(id))
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

        // POST: api/ApplicationOptions
        [HttpPost]
        public async Task<IActionResult> PostApplicationOption([FromBody] ApplicationOption applicationOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ApplicationOptions.Add(applicationOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplicationOption", new { id = applicationOption.OptionsId }, applicationOption);
        }

        // DELETE: api/ApplicationOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationOption([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationOption = await _context.ApplicationOptions.SingleOrDefaultAsync(m => m.OptionsId == id);
            if (applicationOption == null)
            {
                return NotFound();
            }

            _context.ApplicationOptions.Remove(applicationOption);
            await _context.SaveChangesAsync();

            return Ok(applicationOption);
        }
        */
        private bool ApplicationOptionExists(int id)
        {
            return _context.ApplicationOptions.Any(e => e.OptionsId == id);
        }
    }
}