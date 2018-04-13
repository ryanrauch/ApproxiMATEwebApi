using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ZoneRegionPolygons",
                columns: table => new
                {
                    RegionId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneRegionPolygons", x => new { x.RegionId, x.Order });
                    table.ForeignKey(
                        name: "FK_ZoneRegionPolygons_ZoneRegions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "ZoneRegions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZoneRegionPolygons");
        }
    }
}
