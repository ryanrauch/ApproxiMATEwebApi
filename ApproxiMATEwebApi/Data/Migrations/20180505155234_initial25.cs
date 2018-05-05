using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "ApplicationOptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionMajor",
                table: "ApplicationOptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VersionMinor",
                table: "ApplicationOptions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "ApplicationOptions");

            migrationBuilder.DropColumn(
                name: "VersionMajor",
                table: "ApplicationOptions");

            migrationBuilder.DropColumn(
                name: "VersionMinor",
                table: "ApplicationOptions");
        }
    }
}
