using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApproxiMATEwebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/UserPhoneNumbers")]
    [Authorize]
    public class UserPhoneNumbersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserPhoneNumbersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/UserPhoneNumbers
        [HttpPost]
        public async Task<IActionResult> PostUserPhoneNumberFriends([FromBody] UserPhoneNumbers numbers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var applicationUser = _context.ApplicationUser
                                          .SingleOrDefault(a => a.Id == numbers.UserId.ToString());
            if (applicationUser == null)
            {
                return NotFound(numbers.UserId);
            }

            //var registeredUsers = _context.ApplicationUser
            //                              .Where(a => a.PhoneNumber.Equals());
            var registeredUsers = from a in _context.ApplicationUser
                                  where numbers.Numbers.Contains(a.PhoneNumber)
                                  select new UserPhoneResult
                                  {
                                      UserId = Guid.Parse(a.Id),
                                      PhoneNumber = a.PhoneNumber,
                                      UserName = a.UserName
                                  };
            //List of UserPhoneResult.cs should be the return type
            var data = await registeredUsers.ToListAsync();
            return Ok(data);
            // TODO move this to a friendrequest controller
            //var friends = await _context.FriendRequests
            //                            .Where(f => f.InitiatorId.Equals(numbers.UserId) 
            //                                        || f.TargetId.Equals(numbers.UserId))
            //                            .ToListAsync();
            //return Ok(friends);
        }
    }
}