using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_ZoneRegionPolygons_ZoneRegions_RegionId",
                table: "ZoneRegionPolygons",
                column: "RegionId",
                principalTable: "ZoneRegions",
                principalColumn: "RegionId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZoneRegionPolygons_ZoneRegions_RegionId",
                table: "ZoneRegionPolygons");
        }
    }
}
