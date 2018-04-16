﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApproxiMATEwebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/FriendRequests")]
    [Authorize]
    public class FriendRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriendRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/FriendRequests
        /*[HttpGet]
        public IEnumerable<FriendRequest> GetFriendRequests()
        {
            return _context.FriendRequests;
        }*/

        // GET: api/FriendRequests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFriendRequest([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //generated by default scaffolding
            //var friendRequest = await _context.FriendRequests.SingleOrDefaultAsync(m => m.InitiatorId == id);

            //this will pull both initiator and target requests.
            var friendRequest = await _context.FriendRequests.Where(f => f.InitiatorId.Equals(id)
                                                                      || f.TargetId.Equals(id)).ToListAsync();

            if (friendRequest == null)
            {
                return NotFound();
            }

            return Ok(friendRequest);
        }

        // PUT: api/FriendRequests/5
        // this will be used to delete/remove a friendrequest.
        // id is initiator, friendRequest contains the targetUserId
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendRequest([FromRoute] Guid id, [FromBody] FriendRequest friendRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != friendRequest.InitiatorId)
            {
                return BadRequest();
            }

            //_context.Entry(friendRequest).State = EntityState.Modified;
            _context.FriendRequests.Remove(friendRequest);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendRequestExists(id, friendRequest.TargetId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FriendRequests
        [HttpPost]
        public async Task<IActionResult> PostFriendRequest([FromBody] FriendRequest friendRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // don't allow friend requests to self.
            if (friendRequest.InitiatorId.Equals(friendRequest.TargetId))
            {
                return BadRequest(friendRequest.TargetId);
            }
            _context.FriendRequests.Add(friendRequest);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FriendRequestExists(friendRequest.InitiatorId, friendRequest.TargetId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFriendRequest", new { id = friendRequest.InitiatorId }, friendRequest);
        }

        /*
        // DELETE: api/FriendRequests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendRequest([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var friendRequest = await _context.FriendRequests.SingleOrDefaultAsync(m => m.InitiatorId == id);
            if (friendRequest == null)
            {
                return NotFound();
            }

            _context.FriendRequests.Remove(friendRequest);
            await _context.SaveChangesAsync();

            return Ok(friendRequest);
        }
        */

        private bool FriendRequestExists(Guid initiate, Guid target)
        {
            return _context.FriendRequests.Any(e => e.InitiatorId == initiate && e.TargetId == target);
        }
    }
}