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
using ApproxiMATEwebApi.Helpers;

namespace ApproxiMATEwebApi.Controllers.api2
{
    [Produces("application/json")]
    [Route("api/V2CurrentLocation")]
    [Authorize]
    public class V2CurrentLocationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public V2CurrentLocationController(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // POST: api/V2CurrentLocation
        [HttpPost]
        public async Task<IActionResult> PostCurrentLocation([FromBody] CurrentLocationPost currentLocationPost)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var timeStamp = DateTime.Now.ToUniversalTime();
            var gid = _httpContextAccessor.CurrentUserGuid();
            var appUser = await _context.ApplicationUser.FindAsync(gid.ToString());
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
            await _context.SaveChangesAsync();

            IHexagonal hexagonal = new HexagonalEquilateral(currentLocationPost.Latitude, currentLocationPost.Longitude);
            var poly = hexagonal.HexagonalPolygon(hexagonal.CenterLocation);
            return Ok(poly);
        }
    }
}