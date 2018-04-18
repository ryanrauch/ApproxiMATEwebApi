using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Helpers;
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
        private readonly IRegionFunctions _regionFunctions;

        public FriendLocationRegionController(ApplicationDbContext context, 
                                              IRegionFunctions regionFunctions)
        {
            _context = context;
            _regionFunctions = regionFunctions;
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
            var applicationOptions = await _context.ApplicationOptions
                                                    .OrderByDescending(a => a.OptionsDate)
                                                    .FirstOrDefaultAsync();
            if(applicationOptions == null)
            {
                return NotFound("ApplicationOptions");
            }
            var locationRegion = new FriendLocationRegion
            {
                RegionID = region.RegionId,
                Friends = new List<FriendLocationResult>()
            };

            var friends = await _context.FriendRequests
                            .Where(f => f.TargetId.ToString().Equals(applicationUser.Id))
                            .ToListAsync();
            var zoneRegionPolygonsRaw = await _context.ZoneRegionPolygons
                                                      .Where(r => r.RegionId.Equals(region.RegionId))
                                                      .OrderBy(r => r.Order)
                                                      .ToListAsync();
            var latMin = zoneRegionPolygonsRaw.Min(r => r.Latitude);
            var latMax = zoneRegionPolygonsRaw.Max(r => r.Latitude);
            var lonMin = zoneRegionPolygonsRaw.Min(r => r.Longitude);
            var lonMax = zoneRegionPolygonsRaw.Max(r => r.Longitude);
            var friendsInBoundedBox = from a in _context.ApplicationUser
                                      from f in friends
                                      from r in _context.ZoneRegionPolygons
                                      where a.Id.Equals(f.InitiatorId)
                                      where r.RegionId.Equals(region.RegionId)
                                      where a.CurrentLatitude >= latMin
                                      where a.CurrentLatitude < latMax
                                      where a.CurrentLongitude != null
                                      where a.CurrentLongitude >= lonMin
                                      where a.CurrentLongitude < lonMax
                                      where a.CurrentTimeStamp != null
                                      where a.CurrentTimeStamp > DateTime.Now.Subtract(applicationOptions.DataTimeWindow)
                                      select new FriendRequest
                                      {
                                          InitiatorId = f.InitiatorId,
                                          TargetId = f.TargetId,
                                          TimeStamp = f.TimeStamp,
                                          Type = f.Type
                                      };
            //TODO: the above finds the rough bounded box to create a smaller list
            //      still have to run through actual hit-testing algorithm below????
            var currentFriends = from f in friendsInBoundedBox
                                 from a in _context.ApplicationUser
                                 where a.Id.Equals(f.InitiatorId)
                                 //where _regionFunctions.PointOnPolygonExterior(zoneRegionPolygonsRaw,
                                 //                                              a.CurrentLatitude.Value,
                                 //                                              a.CurrentLongitude.Value)
                                 where _regionFunctions.PointInsidePolygon(zoneRegionPolygonsRaw,
                                                                           a.CurrentLatitude.Value,
                                                                           a.CurrentLongitude.Value)
                                 select new FriendLocationResult
                                 {
                                     PhoneNumber = a.PhoneNumber,
                                     UserId = Guid.Parse(a.Id),
                                     Timestamp = a.CurrentTimeStamp.Value,
                                     UserName = a.UserName
                                 };
            locationRegion.Friends = await currentFriends.ToListAsync();
            return Ok(locationRegion);
        }
    }
}