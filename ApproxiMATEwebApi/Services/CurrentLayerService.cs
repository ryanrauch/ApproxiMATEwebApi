using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Models;
using ApproxiMATEwebApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Services
{
    public class CurrentLayerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHexagonal _hexagonal;

        public CurrentLayerService(ApplicationDbContext context, IHexagonal hexagonal)
        {
            _context = context;
            _hexagonal = hexagonal;
        }

        public async Task CreateOrUpdateCurrentLayers(string gid, double latitude, double longitude, DateTime timeStamp)
        {
            _hexagonal.Initialize(latitude, longitude, _hexagonal.Layers[0]);
            String layers = _hexagonal.AllLayersDelimited();
            var currentLayer = await _context.CurrentLayers.FirstOrDefaultAsync(c => c.UserId.Equals(gid));
            if (currentLayer == null)
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
        }
    }
}
