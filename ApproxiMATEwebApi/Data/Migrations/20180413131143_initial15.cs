using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ZoneRegionPolygons_Order_RegionId",
                table: "ZoneRegionPolygons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_ZoneRegionPolygons_Order_RegionId",
                table: "ZoneRegionPolygons",
                columns: new[] { "Order", "RegionId" });
        }
    }
}
