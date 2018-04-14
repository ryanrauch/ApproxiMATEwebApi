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
    [Route("api/FriendLocationBox")]
    [Authorize]
    public class FriendLocationBoxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriendLocationBoxController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/FriendLocationBox
        [HttpPost]
        public async Task<IActionResult> PostFriendLocationBox([FromBody] FriendLocationBoxRequest box)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var applicationUser = _context.ApplicationUser
                                          .SingleOrDefault(a => a.Id == box.UserId.ToString());
            if (applicationUser == null)
            {
                return NotFound(box.UserId);
            }
            var applicationOptions = await _context.ApplicationOptions
                                                    .OrderByDescending(a => a.OptionsDate)
                                                    .FirstOrDefaultAsync();
            if(applicationOptions == null)
            {
                return NotFound("ApplicationOptions");
            }
            var locationBox = new FriendLocationBox
            {
                BoundingBox = box.BoundingBox,
                Friends = new List<FriendLocationResult>()
            };
            double latMin = CoordinateFilters.GetLatitudeFloorFromBox(locationBox.BoundingBox),
                   latMax = CoordinateFilters.GetLatitudeCeilingFromBox(locationBox.BoundingBox),
                   lonMin = CoordinateFilters.GetLongitudeFloorFromBox(locationBox.BoundingBox),
                   lonMax = CoordinateFilters.GetLongitudeCeilingFromBox(locationBox.BoundingBox);
            var friends = await _context.FriendRequests
                                        .Where(f => f.TargetId.ToString().Equals(applicationUser.Id))
                                        .ToListAsync();
            var currentFriends = from a in _context.ApplicationUser
                                 from f in _context.FriendRequests
                                 where a.Id.Equals(f.InitiatorId.ToString())
                                 where a.CurrentLatitude != null
                                 where a.CurrentLatitude >= latMin
                                 where a.CurrentLatitude < latMax
                                 where a.CurrentLongitude != null
                                 where a.CurrentLongitude >= lonMin
                                 where a.CurrentLongitude < lonMax
                                 where a.CurrentTimeStamp != null
                                 where a.CurrentTimeStamp > DateTime.Now.Subtract(applicationOptions.DataTimeWindow)
                                 select new FriendLocationResult
                                 {
                                     PhoneNumber = a.PhoneNumber,
                                     UserId = Guid.Parse(a.Id),
                                     Timestamp = a.CurrentTimeStamp.Value,
                                     UserName = a.UserName
                                 };
            locationBox.Friends = await currentFriends.ToListAsync();
            return Ok(locationBox);
        }
    }
}