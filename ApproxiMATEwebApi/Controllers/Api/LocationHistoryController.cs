using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using Microsoft.AspNetCore.Authorization;
using ApproxiMATEwebApi.Extensions;

namespace ApproxiMATEwebApi.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/LocationHistory")]
    [Authorize]
    public class LocationHistoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocationHistoryController(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/LocationHistory
        [HttpGet]
        public async Task<IActionResult> GetAllLocationHistory()
        {
            var target = _httpContextAccessor.CurrentUserId();
            var history = await _context.LocationHistory
                                        .Where(h => h.UserId.Equals(target))
                                        .ToListAsync();
            if (history.Count == 0)
            {
                return NoContent();
            }
            return Ok(history);
        }

        // GET: api/LocationHistory/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationHistory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _httpContextAccessor.CurrentUserId();
            var locationHistory = await _context.LocationHistory
                                                .SingleOrDefaultAsync(m => m.HistoryID.ToString().Equals(id)
                                                                           && m.UserId.Equals(userId));
            if (locationHistory == null)
            {
                return NotFound();
            }
            return Ok(locationHistory);
        }

        // POST: api/LocationHistories
        [HttpPost]
        public async Task<IActionResult> PostLocationHistory([FromBody] LocationHistory locationHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _httpContextAccessor.CurrentUserId();
            if(!locationHistory.UserId.Equals(userId))
            {
                return Unauthorized();
            }
            locationHistory.TimeStamp = DateTime.Now.ToUniversalTime();
            _context.LocationHistory.Add(locationHistory);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetLocationHistory", new { id = locationHistory.HistoryID }, locationHistory);
        }
    }
}