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

        public async Task CreateLocationHistoryAsync(LocationHistory history)
        {
            await _context.LocationHistory
                          .AddAsync(history);
            await _context.SaveChangesAsync();
        }

        public async Task CreateLocationHistoryAsync(string id, double latitude, double longitude, DateTime timeStamp)
        {
            await CreateLocationHistoryAsync(new LocationHistory()
                                             {
                                                 UserId = id,
                                                 Latitude = latitude,
                                                 Longitude = longitude,
                                                 TimeStamp = timeStamp
                                             });
        }
    }
}
