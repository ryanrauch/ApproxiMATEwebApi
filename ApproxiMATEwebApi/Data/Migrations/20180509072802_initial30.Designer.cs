﻿// <auto-generated />
using ApproxiMATEwebApi.Data;
using ApproxiMATEwebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ApproxiMATEwebApi.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180509072802_initial30")]
    partial class initial30
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApproxiMATEwebApi.Models.ApplicationOption", b =>
                {
                    b.Property<int>("OptionsId")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("DataTimeWindow");

                    b.Property<string>("EndUserLicenseAgreementSource");

                    b.Property<DateTime>("OptionsDate");

                    b.Property<string>("PrivacyPolicySource");

                    b.Property<string>("TermsConditionsSource");

                    b.Property<int>("Version");

                    b.Property<int>("VersionMajor");

                    b.Property<int>("VersionMinor");

                    b.HasKey("OptionsId");

                    b.ToTable("ApplicationOptions");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("AccountType");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<double?>("CurrentLatitude");

                    b.Property<double?>("CurrentLongitude");

                    b.Property<DateTime?>("CurrentTimeStamp");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<DateTime?>("TermsAndConditionsDate");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.CurrentLayer", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LayersDelimited");

                    b.Property<DateTime>("TimeStamp");

                    b.HasKey("UserId");

                    b.ToTable("CurrentLayers");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.FriendRequest", b =>
                {
                    b.Property<string>("InitiatorId");

                    b.Property<string>("TargetId");

                    b.Property<bool>("TargetViewed");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<int?>("Type");

                    b.HasKey("InitiatorId", "TargetId");

                    b.HasIndex("TargetId");

                    b.ToTable("FriendRequests");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.LocationHistory", b =>
                {
                    b.Property<int>("HistoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<DateTime>("TimeStamp");

                    b.Property<string>("UserId");

                    b.HasKey("HistoryID");

                    b.HasIndex("UserId");

                    b.ToTable("LocationHistories");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.ZoneCity", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int?>("StateId");

                    b.HasKey("CityId");

                    b.HasIndex("StateId");

                    b.ToTable("ZoneCities");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.ZoneRegion", b =>
                {
                    b.Property<int>("RegionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ARGBFill");

                    b.Property<string>("ARGBStroke");

                    b.Property<double>("BoundLatitudeMax");

                    b.Property<double>("BoundLatitudeMin");

                    b.Property<double>("BoundLongitudeMax");

                    b.Property<double>("BoundLongitudeMin");

                    b.Property<int?>("CityId");

                    b.Property<string>("Description");

                    b.Property<float>("StrokeWidth");

                    b.Property<int>("Type");

                    b.HasKey("RegionId");

                    b.HasIndex("CityId");

                    b.ToTable("ZoneRegions");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.ZoneRegionPolygon", b =>
                {
                    b.Property<int>("RegionId");

                    b.Property<int>("Order");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.HasKey("RegionId", "Order");

                    b.ToTable("ZoneRegionPolygons");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.ZoneState", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ShortDescription");

                    b.HasKey("StateId");

                    b.ToTable("ZoneStates");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.FriendRequest", b =>
                {
                    b.HasOne("ApproxiMATEwebApi.Models.ApplicationUser", "Initiator")
                        .WithMany()
                        .HasForeignKey("InitiatorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApproxiMATEwebApi.Models.ApplicationUser", "Target")
                        .WithMany()
                        .HasForeignKey("TargetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.LocationHistory", b =>
                {
                    b.HasOne("ApproxiMATEwebApi.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.ZoneCity", b =>
                {
                    b.HasOne("ApproxiMATEwebApi.Models.ZoneState", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.ZoneRegion", b =>
                {
                    b.HasOne("ApproxiMATEwebApi.Models.ZoneCity", "City")
                        .WithMany()
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("ApproxiMATEwebApi.Models.ZoneRegionPolygon", b =>
                {
                    b.HasOne("ApproxiMATEwebApi.Models.ZoneRegion", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ApproxiMATEwebApi.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ApproxiMATEwebApi.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApproxiMATEwebApi.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ApproxiMATEwebApi.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
