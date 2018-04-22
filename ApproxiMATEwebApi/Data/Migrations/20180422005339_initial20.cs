using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BoundLatitudeMax",
                table: "ZoneRegions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BoundLatitudeMin",
                table: "ZoneRegions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BoundLongitudeMax",
                table: "ZoneRegions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BoundLongitudeMin",
                table: "ZoneRegions",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoundLatitudeMax",
                table: "ZoneRegions");

            migrationBuilder.DropColumn(
                name: "BoundLatitudeMin",
                table: "ZoneRegions");

            migrationBuilder.DropColumn(
                name: "BoundLongitudeMax",
                table: "ZoneRegions");

            migrationBuilder.DropColumn(
                name: "BoundLongitudeMin",
                table: "ZoneRegions");
        }
    }
}
