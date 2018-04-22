using ApproxiMATEwebApi.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Data
{
    public class DataInitializer
    {
        ApplicationDbContext _context { get; set; }

        public async Task InitializeDataAsync(IServiceProvider serviceProvider)
        {
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            // Add the code for inintializing at here
            if (_context.ApplicationOptions.Count() == 0)
            {
                _context.ApplicationOptions.Add(new ApplicationOption()
                {
                    OptionsDate = DateTime.Now,
                    EndUserLicenseAgreementSource = "https://www.lipsum.com/",
                    PrivacyPolicySource = "https://www.lipsum.com/",
                    TermsConditionsSource = "https://www.lipsum.com/"
                });
                await _context.SaveChangesAsync();
            }
            //return;
            ZoneCity zoneCityAustin = _context.ZoneCities.FirstOrDefault(c => c.Description.Equals("austin", StringComparison.OrdinalIgnoreCase));
            if(zoneCityAustin != null && _context.ZoneRegions.Count() == 0)
            {
                var northAustin = new ZoneRegion()
                {
                    City = zoneCityAustin,
                    Description = "North",
                    Type = (int)RegionType.Neighborhood,
                    RGBColorHex = "FDCAC9"
                };
                _context.ZoneRegions.Add(northAustin);
                int i = 0;
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.468183, Longitude = -97.796568, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.481202, Longitude = -97.744093, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.475876, Longitude = -97.702605, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.479723, Longitude = -97.673583, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.454274, Longitude = -97.66677, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.407798, Longitude = -97.674034, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.381145, Longitude = -97.674087, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.33819, Longitude = -97.69989, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.347079, Longitude = -97.712304, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.368705, Longitude = -97.71888, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.379072, Longitude = -97.73816, RegionId = northAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = northAustin, Order = ++i, Latitude = 30.447467, Longitude = -97.790109, RegionId = northAustin.RegionId });

                var pflugervilleAustin = new ZoneRegion()
                {
                    City = zoneCityAustin,
                    Description = "Pflugerville",
                    Type = (int)RegionType.Neighborhood,
                    RGBColorHex = "99FAD7"
                };
                i = 0;
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.479723, Longitude = -97.673583, RegionId = pflugervilleAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.488303, Longitude = -97.633951, RegionId = pflugervilleAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.472029, Longitude = -97.592119, RegionId = pflugervilleAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.388846, Longitude = -97.57844, RegionId = pflugervilleAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.351819, Longitude = -97.592098, RegionId = pflugervilleAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.366039, Longitude = -97.605541, RegionId = pflugervilleAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.388549, Longitude = -97.651256, RegionId = pflugervilleAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.400987, Longitude = -97.655484, RegionId = pflugervilleAustin.RegionId });
                //shared with north austin
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.407798, Longitude = -97.674034, RegionId = pflugervilleAustin.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = pflugervilleAustin, Order = ++i, Latitude = 30.454274, Longitude = -97.66677, RegionId = pflugervilleAustin.RegionId });

                await _context.SaveChangesAsync();
            }
        }
    }
}
