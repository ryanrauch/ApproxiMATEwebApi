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
    [Route("api/CurrentLocation")]
    [Authorize]
    public class CurrentLocationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrentLocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrentLocation([FromRoute] string id, [FromBody] CurrentLocation currentLocation)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var applicationUser = _context.ApplicationUser
                                          .SingleOrDefault(a => a.Id == id);
            if (applicationUser == null)
            {
                return NotFound(id);
            }
            DateTime timeStamp = DateTime.Now.ToUniversalTime();
            applicationUser.CurrentLatitude = currentLocation.Latitude;
            applicationUser.CurrentLongitude = currentLocation.Longitude;
            applicationUser.CurrentTimeStamp = timeStamp;
            //_context.Update(applicationUser);
            //_context.Entry(applicationUser).State = EntityState.Modified;
            await _context.LocationHistory
                          .AddAsync(new LocationHistory()
                          {
                              User = applicationUser,
                              Latitude = currentLocation.Latitude,
                              Longitude = currentLocation.Longitude,
                              TimeStamp = timeStamp
                          });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}