using ApproxiMATEwebApi.Helpers;
using ApproxiMATEwebApi.Models;
using ApproxiMATEwebApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApproxiMATEwebApi.Data
{
    public class DataInitializer
    {
        private ApplicationDbContext _context { get; set; }
        private UserManager<ApplicationUser> _userManager { get; set; }
        private IHexagonal _hexagonal { get; set; }

        public async Task InitializeMockUsers(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            _hexagonal = serviceProvider.GetRequiredService<IHexagonal>();

            //create the mock users if they don't exist
            var mock = await _userManager.FindByEmailAsync("Mock01@RyanRauch.com");
            if(mock == null)
            {
                for(int i = 1; i <= 25; ++i)
                {
                    string mockFirst = String.Format("Mock{0}", i.ToString("D2"));
                    string mockLast = String.Format("Data{0}", i.ToString("D2"));
                    string mockMail = String.Format("{0}@RyanRauch.com", mockFirst);
                    string mockNumber = String.Format("55512300{0}", i.ToString("D2"));
                    string mockPass = String.Format("Password{0}!", i.ToString("D2"));
                    var user = new ApplicationUser
                    {
                        UserName = mockFirst + mockLast,
                        Email = mockMail,
                        DateOfBirth = DateTime.Now.Date.AddYears(-30).Subtract(TimeSpan.FromDays(30*i)),
                        FirstName = mockFirst,
                        LastName = mockLast,
                        PhoneNumber = ExtractPhoneNumber.RemoveNonNumeric(mockNumber),
                        AccountType = AccountType.MockedData,
                        Gender = i % 2 == 0 ? AccountGender.Male : AccountGender.Female
                    };
                    var result = await _userManager.CreateAsync(user, mockPass);
                }
            }

            //update current location data for mock users
            /////////////////////////////////////////////
            double latmin = 30.3740;
            double latmax = 30.4251;
            double lonmin = -97.7501;
            double lonmax = -97.7001;
            Random randomLat = new Random((int)DateTime.Now.Ticks);
            Random randomLon = new Random((int)DateTime.Now.Ticks);
            Random randomMin = new Random((int)DateTime.Now.Ticks);
            var mockedUsers = await _context.ApplicationUser.Where(a => a.AccountType.Equals(AccountType.MockedData)).ToListAsync();
            foreach(var user in mockedUsers)
            {
                DateTime timeStamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(randomMin.NextDouble() * 60));
                double lat = randomLat.NextDouble() * (latmax - latmin) + latmin;
                double lon = randomLon.NextDouble() * (lonmax - lonmin) + lonmin;
                user.CurrentTimeStamp = timeStamp;
                user.CurrentLatitude = lat;
                user.CurrentLongitude = lon;
                _hexagonal.Initialize(lat, lon, _hexagonal.Layers[0]);
                String layers = _hexagonal.AllLayersDelimited();
                var currentLayer = await _context.CurrentLayers.FirstOrDefaultAsync(c => c.UserId.Equals(Guid.Parse(user.Id)));
                if (currentLayer == null)
                {
                    await _context.CurrentLayers.AddAsync(new CurrentLayer()
                    {
                        UserId = Guid.Parse(user.Id),
                        LayersDelimited = layers,
                        TimeStamp = timeStamp
                    });
                }
                else
                {
                    currentLayer.LayersDelimited = layers;
                    currentLayer.TimeStamp = timeStamp;
                }
            }
            await _context.SaveChangesAsync();

            //establish friend-requests for all of the mock users
            //////////////////////////////////////////
            var ryan = await _context.ApplicationUser
                                     .FirstOrDefaultAsync(a => a.Email.Equals("rauch.ryan@gmail.com", StringComparison.OrdinalIgnoreCase));
            mockedUsers = await _context.ApplicationUser
                                        .Where(a => a.AccountType.Equals(AccountType.MockedData))
                                        .ToListAsync();
            foreach (var initiator in mockedUsers)
            {
                DateTime timeStamp = DateTime.Now.Subtract(TimeSpan.FromMinutes(randomMin.NextDouble() * 60));
                var friendRequest = await _context.FriendRequests
                                                  .FirstOrDefaultAsync(f => f.InitiatorId.Equals(initiator.Id)
                                                                            && f.TargetId.Equals(ryan.Id));
                if (friendRequest == null)
                {
                    await _context.FriendRequests
                                  .AddAsync(new FriendRequest()
                                  {
                                      InitiatorId = initiator.Id,
                                      TargetId = ryan.Id,
                                      TimeStamp = DateTime.Now,
                                      Type = FriendRequestType.Normal
                                  });
                }
                else
                {
                    friendRequest.TimeStamp = timeStamp;
                }
            }
            await _context.SaveChangesAsync();
        }
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

            // Zone Data
            if(_context.ZoneRegions.Count() == 0)
            {
                if(_context.ZoneStates.Count() == 0)
                {
                    _context.ZoneStates.Add(new ZoneState()
                    {
                        //StateId=1,
                        Description="Texas",
                        ShortDescription="TX"
                    });
                }
                await _context.SaveChangesAsync();
                var texas = _context.ZoneStates.FirstOrDefault(s => s.Description.Equals("Texas", StringComparison.OrdinalIgnoreCase));
                if(_context.ZoneCities.Count() == 0)
                {
                    _context.ZoneCities.Add(new ZoneCity()
                    {
                        //CityId=1,
                        Description="Austin",
                        State = texas
                    });
                    await _context.SaveChangesAsync();
                }
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
