using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Services
{
    public class LocationHistoryService
    {
        private readonly ApplicationDbContext _context;

        public LocationHistoryService(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateLocationHistory(string id, double latitude, double longitude, DateTime timeStamp)
        {
            await _context.LocationHistory
                           .AddAsync(new LocationHistory()
                           {
                               UserId = id,
                               Latitude = latitude,
                               Longitude = longitude,
                               TimeStamp = timeStamp
                           });
        }
    }
}
