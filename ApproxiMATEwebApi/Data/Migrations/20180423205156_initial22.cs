using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RGBColorHex",
                table: "ZoneRegions",
                newName: "RGBAStroke");

            migrationBuilder.AddColumn<string>(
                name: "RGBAFill",
                table: "ZoneRegions",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "StrokeWidth",
                table: "ZoneRegions",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RGBAFill",
                table: "ZoneRegions");

            migrationBuilder.DropColumn(
                name: "StrokeWidth",
                table: "ZoneRegions");

            migrationBuilder.RenameColumn(
                name: "RGBAStroke",
                table: "ZoneRegions",
                newName: "RGBColorHex");
        }
    }
}
