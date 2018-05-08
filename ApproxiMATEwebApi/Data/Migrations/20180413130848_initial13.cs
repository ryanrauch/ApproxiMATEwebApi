using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZoneRegionPolygons");

            migrationBuilder.CreateTable(
                name: "ZoneRegionPolygons",
                columns: table => new
                {
                    Order = table.Column<int>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    RegionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneRegionPolygons", x => x.Order);
                    table.ForeignKey(
                        name: "FK_ZoneRegionPolygons_ZoneRegions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "ZoneRegions",
                        principalColumn: "RegionId",
                        onDelete: ReferentialAction.Restrict);
                });
            //???

            migrationBuilder.DropForeignKey(
                name: "FK_ZoneRegionPolygons_ZoneRegions_RegionId",
                table: "ZoneRegionPolygons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneRegionPolygons",
                table: "ZoneRegionPolygons");

            /*migrationBuilder.DropIndex(
                name: "IX_ZoneRegionPolygons_RegionId",
                table: "ZoneRegionPolygons");
                */
            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "ZoneRegionPolygons",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            /*migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "ZoneRegionPolygons",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
                */
            migrationBuilder.AddUniqueConstraint(
                name: "AK_ZoneRegionPolygons_Order_RegionId",
                table: "ZoneRegionPolygons",
                columns: new[] { "Order", "RegionId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneRegionPolygons",
                table: "ZoneRegionPolygons",
                columns: new[] { "RegionId", "Order" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ZoneRegionPolygons_Order_RegionId",
                table: "ZoneRegionPolygons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZoneRegionPolygons",
                table: "ZoneRegionPolygons");

            migrationBuilder.AlterColumn<int>(
                name: "Order",
                table: "ZoneRegionPolygons",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "RegionId",
                table: "ZoneRegionPolygons",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZoneRegionPolygons",
                table: "ZoneRegionPolygons",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneRegionPolygons_RegionId",
                table: "ZoneRegionPolygons",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ZoneRegionPolygons_ZoneRegions_RegionId",
                table: "ZoneRegionPolygons",
                column: "RegionId",
                principalTable: "ZoneRegions",
                principalColumn: "RegionId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
