using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ApproxiMATEwebApi.Models;

namespace ApproxiMATEwebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<LocationHistory> LocationHistories { get; set; }
        public DbSet<ZoneCity> ZoneCities { get; set; }
        public DbSet<ZoneRegion> ZoneRegions { get; set; }
        public DbSet<ZoneRegionPolygon> ZoneRegionPolygons { get; set; }
        public DbSet<ZoneState> ZoneStates { get; set; }
        public DbSet<ApproxiMATEwebApi.Models.ApplicationUser> ApplicationUser { get; set; }
    }
}
