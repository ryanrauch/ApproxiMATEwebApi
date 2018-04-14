using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApproxiMATEwebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/FriendLocationRegion")]
    [Authorize]
    public class FriendLocationRegionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriendLocationRegionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostFriendLocationRegion([FromBody] FriendLocationRegionRequest region)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var applicationUser = _context.ApplicationUser
                                          .SingleOrDefault(a => a.Id == region.UserId.ToString());
            if (applicationUser == null)
            {
                return NotFound(region.UserId);
            }
            var applicationOptioins = await _context.ApplicationOptions
                                                    .OrderByDescending(a => a.OptionsDate)
                                                    .FirstOrDefaultAsync();
            if(applicationOptioins == null)
            {
                return NotFound("ApplicationOptions");
            }
            var locationRegion = new FriendLocationRegion
            {
                RegionID = region.RegionId,
                Friends = new List<FriendLocationResult>()
            };

            //TODO: Implement Ray-casting algorithm
            //      to determine if users are within the specific region
            return NotFound("splution for region polygons");

            return Ok(locationRegion);
        }
    }
}