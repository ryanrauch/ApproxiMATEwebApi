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
            builder.Entity<FriendRequest>()
                   .HasKey(f => new { f.InitiatorId, f.TargetId });
            builder.Entity<ZoneRegionPolygon>()
                   .HasKey(p => new { p.RegionId, p.Order });
        }
        public DbSet<LocationHistory> LocationHistories { get; set; }
        public DbSet<ZoneCity> ZoneCities { get; set; }
        public DbSet<ZoneRegion> ZoneRegions { get; set; }
        public DbSet<ZoneRegionPolygon> ZoneRegionPolygons { get; set; }//TODO: accidentally dropped in sql- not sure how to add this back
        public DbSet<ZoneState> ZoneStates { get; set; }
        public DbSet<ApproxiMATEwebApi.Models.ApplicationUser> ApplicationUser { get; set; }

        public DbSet<FriendRequest> FriendRequests { get; set; }

        public DbSet<ApplicationOption> ApplicationOptions { get; set; }
        public DbSet<CurrentLayer> CurrentLayers { get; set; }
    }
}
