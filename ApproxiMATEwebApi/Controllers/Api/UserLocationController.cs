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

        // POST: apiV2/UserLocation
        [HttpPost]
        public async Task<IActionResult> PostCurrentLocation([FromBody] CurrentLocationPost currentLocationPost)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var timeStamp = DateTime.Now.ToUniversalTime();
            var gid = _httpContextAccessor.CurrentUserGuid();
            var appUser = await _context.ApplicationUser.SingleOrDefaultAsync(a=>a.Id.Equals(gid.ToString()));
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
            var currentLayer = await _context.CurrentLayers.FirstOrDefaultAsync(c => c.UserId.Equals(gid));
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
                //_context.Entry(currentLayer).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return Ok();
            //var poly = _hexagonal.HexagonalPolygon(_hexagonal.CenterLocation);
            //return Ok(poly);
        }
    }
}