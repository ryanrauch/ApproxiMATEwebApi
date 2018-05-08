using ApproxiMATEwebApi.Helpers;
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
                    TermsConditionsSource = "https://www.lipsum.com/",
                    DataTimeWindow = TimeSpan.FromHours(12),
                    Version=1,
                    VersionMajor=0,
                    VersionMinor=1
                });
                await _context.SaveChangesAsync();
            }
            //return;
            if(_context.ZoneRegions.Count() == 0)
            {
                ZoneCity zoneCityAustin = _context.ZoneCities.FirstOrDefault(c => c.Description.Equals("austin", StringComparison.OrdinalIgnoreCase));
                if (zoneCityAustin == null)
                    return;
                var northAustin = new ZoneRegion()
                {
                    City = zoneCityAustin,
                    Description = "North",
                    Type = (int)RegionType.Neighborhood,
                    ARGBFill = "80FDCAC9",
                    ARGBStroke = "00000000",
                    StrokeWidth = 1.0f
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
                //northAustin.BoundLatitudeMin = _context.ZoneRegionPolygons.Where(r => r.RegionId.Equals(northAustin.RegionId)).Min(r => r.Latitude);
                //northAustin.BoundLatitudeMax = _context.ZoneRegionPolygons.Where(r => r.RegionId.Equals(northAustin.RegionId)).Max(r => r.Latitude);
                //northAustin.BoundLongitudeMin = _context.ZoneRegionPolygons.Where(r => r.RegionId.Equals(northAustin.RegionId)).Min(r => r.Longitude);
                //northAustin.BoundLongitudeMax = _context.ZoneRegionPolygons.Where(r => r.RegionId.Equals(northAustin.RegionId)).Max(r => r.Longitude);
                var pflugervilleAustin = new ZoneRegion()
                {
                    City = zoneCityAustin,
                    Description = "Pflugerville",
                    Type = (int)RegionType.Neighborhood,
                    ARGBFill = "8099FAD7",
                    ARGBStroke = "00000000",
                    StrokeWidth = 1.0f
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
                //pflugervilleAustin.BoundLatitudeMin = _context.ZoneRegionPolygons.Where(r => r.RegionId.Equals(pflugervilleAustin.RegionId)).Min(r => r.Latitude);
                //pflugervilleAustin.BoundLatitudeMax = _context.ZoneRegionPolygons.Where(r => r.RegionId.Equals(pflugervilleAustin.RegionId)).Max(r => r.Latitude);
                //pflugervilleAustin.BoundLongitudeMin = _context.ZoneRegionPolygons.Where(r => r.RegionId.Equals(pflugervilleAustin.RegionId)).Min(r => r.Longitude);
                //pflugervilleAustin.BoundLongitudeMax = _context.ZoneRegionPolygons.Where(r => r.RegionId.Equals(pflugervilleAustin.RegionId)).Max(r => r.Longitude);

                var westSixth = new ZoneRegion()
                {
                    City = zoneCityAustin,
                    Description = "West 6th",
                    Type = (int)RegionType.SocialDistrict,
                    ARGBFill = "8095C6E4", //blue-grey
                    ARGBStroke = "00000000",
                    StrokeWidth = 1.0f
                };
                i = 0;
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = westSixth, Order = ++i, Latitude = 30.273021, Longitude = -97.749524, RegionId = westSixth.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = westSixth, Order = ++i, Latitude = 30.271798, Longitude = -97.745204, RegionId = westSixth.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = westSixth, Order = ++i, Latitude = 30.268091, Longitude = -97.746655, RegionId = westSixth.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = westSixth, Order = ++i, Latitude = 30.269296, Longitude = -97.750918, RegionId = westSixth.RegionId });

                var warehouseDistrict = new ZoneRegion()
                {
                    City = zoneCityAustin,
                    Description = "Warehouse District",
                    Type = (int)RegionType.SocialDistrict,
                    ARGBFill = "80D2B7D8", //purple-ish
                    ARGBStroke = "00000000",
                    StrokeWidth = 1.0f
                };
                i = 0;
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = warehouseDistrict, Order = ++i, Latitude = 30.269036, Longitude = -97.74634, RegionId = warehouseDistrict.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = warehouseDistrict, Order = ++i, Latitude = 30.268019, Longitude = -97.742779, RegionId = warehouseDistrict.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = warehouseDistrict, Order = ++i, Latitude = 30.26522, Longitude = -97.743823, RegionId = warehouseDistrict.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = warehouseDistrict, Order = ++i, Latitude = 30.266814, Longitude = -97.749481, RegionId = warehouseDistrict.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = warehouseDistrict, Order = ++i, Latitude = 30.269279, Longitude = -97.750911, RegionId = warehouseDistrict.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = warehouseDistrict, Order = ++i, Latitude = 30.268091, Longitude = -97.746655, RegionId = warehouseDistrict.RegionId });

                var secondStreet = new ZoneRegion()
                {
                    City = zoneCityAustin,
                    Description = "2nd Street",
                    Type = (int)RegionType.SocialDistrict,
                    ARGBFill = "806F7FBD", //purple-ish
                    ARGBStroke = "00000000",
                    StrokeWidth = 1.0f
                };
                i = 0;
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = secondStreet, Order = ++i, Latitude = 30.266517, Longitude = -97.748421, RegionId = secondStreet.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = secondStreet, Order = ++i, Latitude = 30.26522, Longitude = -97.743823, RegionId = secondStreet.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = secondStreet, Order = ++i, Latitude = 30.263367, Longitude = -97.744544, RegionId = secondStreet.RegionId });
                _context.ZoneRegionPolygons.Add(new ZoneRegionPolygon() { Region = secondStreet, Order = ++i, Latitude = 30.264683, Longitude = -97.749128, RegionId = secondStreet.RegionId });

                await _context.SaveChangesAsync();
            }
        }
    }
}
