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

namespace ApproxiMATEwebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/FriendRequest")]
    [Authorize]
    public class FriendRequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FriendRequestController(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: api/FriendRequest
        [HttpGet]
        public async Task<IActionResult> GetAllFriendRequests()
        {
            var userId = _httpContextAccessor.CurrentUserId();
            var friends = await _context.FriendRequests
                                        .Where(f => f.InitiatorId.Equals(userId)
                                                    || f.TargetId.Equals(userId))
                                        .ToListAsync();
            if(friends == null || friends.Count == 0)
            {
                return NoContent();
            }
            return Ok(friends);
        }

        // GET: api/FriendRequests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFriendRequest([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _httpContextAccessor.CurrentUserId();
            var friendRequest = await _context.FriendRequests
                                              .Where(f => (f.InitiatorId.Equals(id) && f.TargetId.Equals(userId))
                                                          || (f.TargetId.Equals(id) && f.InitiatorId.Equals(userId))).ToListAsync();
            if (friendRequest == null)
            {
                return NoContent();
            }
            return Ok(friendRequest);
        }

        // POST: api/FriendRequest
        [HttpPost]
        public async Task<IActionResult> PostFriendRequest([FromBody] FriendRequest friendRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _httpContextAccessor.CurrentUserId();
            if(!friendRequest.InitiatorId.Equals(userId))
            {
                return Unauthorized();
            }
            if (friendRequest.InitiatorId.Equals(friendRequest.TargetId))
            {
                return BadRequest(friendRequest.TargetId);
            }
            var userExists = await _context.ApplicationUser
                                       .AnyAsync(a => a.Id.Equals(friendRequest.TargetId));
            if(!userExists)
            {
                return BadRequest(friendRequest.TargetId);
            }
            var friend = await _context.FriendRequests
                                       .SingleOrDefaultAsync(f => f.InitiatorId.Equals(userId)
                                                                  && f.TargetId.Equals(friendRequest.TargetId));
            DateTime timeStamp = DateTime.Now.ToUniversalTime();
            if (friend != null)
            {
                friend.Type = friendRequest.Type;
                friend.TimeStamp = timeStamp;
            }
            else
            {
                friendRequest.TimeStamp = timeStamp;
                _context.FriendRequests.Add(friendRequest);
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetFriendRequest", new { id = friendRequest.TargetId }, friendRequest);
        }

        // DELETE: api/FriendRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendRequest([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = _httpContextAccessor.CurrentUserId();
            var friendRequest = await _context.FriendRequests
                                              .SingleOrDefaultAsync(m => m.InitiatorId.Equals(userId)
                                                                         && m.TargetId.Equals(id));
            if (friendRequest == null)
            {
                return NoContent();
            }
            _context.FriendRequests.Remove(friendRequest);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}