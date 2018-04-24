using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RGBAStroke",
                table: "ZoneRegions",
                newName: "ARGBStroke");

            migrationBuilder.RenameColumn(
                name: "RGBAFill",
                table: "ZoneRegions",
                newName: "ARGBFill");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ARGBStroke",
                table: "ZoneRegions",
                newName: "RGBAStroke");

            migrationBuilder.RenameColumn(
                name: "ARGBFill",
                table: "ZoneRegions",
                newName: "RGBAFill");
        }
    }
}
