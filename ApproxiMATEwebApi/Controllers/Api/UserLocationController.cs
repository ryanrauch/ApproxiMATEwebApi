using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using ApproxiMATEwebApi.Models.DataContracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApproxiMATEwebApi.Extensions;
using ApproxiMATEwebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApproxiMATEwebApi.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/UserLocation")]
    [Authorize]
    public class UserLocationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHexagonal _hexagonal;

        public UserLocationController(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IHexagonal hexagonal)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _hexagonal = hexagonal;
        }

        // GET: api/UserLocation
        [HttpGet]
        public async Task<IActionResult> GetUserLocations()
        {
            var target = _httpContextAccessor.CurrentUserId();
            //var layers = await from f in _context.FriendRequests
            //                   join c in _context.CurrentLayers on f.TargetId equals c.UserId
            //                   where
            //return Ok(layers);
            return Ok();
        }

        // GET: api/UserLocation/5
        [HttpGet("id")]
        public async Task<IActionResult> GetUserLocation([FromRoute] Guid id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var target = _httpContextAccessor.CurrentUserGuid();
            var friendPermission = await _context.FriendRequests
                                                 .SingleOrDefaultAsync(s => s.TargetId.Equals(target) 
                                                                            && s.InitiatorId.Equals(id));
            if(friendPermission == null)
            {
                return Unauthorized();
            }
            var layer = await _context.CurrentLayers
                                      .SingleOrDefaultAsync(s => s.UserId.Equals(id));
            if(layer == null)
            {
                return NoContent();
            }
            return Ok(layer);
        }

        // POST: api/UserLocation
        [HttpPost]
        public async Task<IActionResult> PostCurrentLocation([FromBody] CurrentLocationPost currentLocationPost)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var timeStamp = DateTime.Now.ToUniversalTime();
            var gid = _httpContextAccessor.CurrentUserId();
            var appUser = await _context.ApplicationUser
                                        .SingleOrDefaultAsync(a=>a.Id.Equals(gid));
            if(appUser == null)
            {
                return NotFound(gid);
            }
            appUser.CurrentLatitude = currentLocationPost.Latitude;
            appUser.CurrentLongitude = currentLocationPost.Longitude;
            appUser.CurrentTimeStamp = timeStamp;
            await _context.LocationHistories
                          .AddAsync(new LocationHistory()
                          {
                              User = appUser,
                              Latitude = currentLocationPost.Latitude,
                              Longitude = currentLocationPost.Longitude,
                              TimeStamp = timeStamp
                          });
            _hexagonal.Initialize(currentLocationPost.Latitude, currentLocationPost.Longitude, _hexagonal.Layers[0]);
            String layers = _hexagonal.AllLayersDelimited();
            var currentLayer = await _context.CurrentLayers
                                             .FirstOrDefaultAsync(c => c.UserId.Equals(gid));
            if(currentLayer == null)
            {
                await _context.CurrentLayers.AddAsync(new CurrentLayer()
                                                      {
                                                          UserId = gid,
                                                          LayersDelimited = layers,
                                                          TimeStamp = timeStamp
                                                       });
            }
            else
            {
                currentLayer.LayersDelimited = layers;
                currentLayer.TimeStamp = timeStamp;
            }
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}