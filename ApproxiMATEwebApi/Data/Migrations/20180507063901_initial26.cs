using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentLayers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Layer1Latitude = table.Column<double>(nullable: false),
                    Layer1Longitude = table.Column<double>(nullable: false),
                    Layer2187Latitude = table.Column<double>(nullable: false),
                    Layer2187Longitude = table.Column<double>(nullable: false),
                    Layer243Latitude = table.Column<double>(nullable: false),
                    Layer243Longitude = table.Column<double>(nullable: false),
                    Layer27Latitude = table.Column<double>(nullable: false),
                    Layer27Longitude = table.Column<double>(nullable: false),
                    Layer3Latitude = table.Column<double>(nullable: false),
                    Layer3Longitude = table.Column<double>(nullable: false),
                    Layer729Latitude = table.Column<double>(nullable: false),
                    Layer729Longitude = table.Column<double>(nullable: false),
                    Layer81Latitude = table.Column<double>(nullable: false),
                    Layer81Longitude = table.Column<double>(nullable: false),
                    Layer9Latitude = table.Column<double>(nullable: false),
                    Layer9Longitude = table.Column<double>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentLayers", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentLayers");
        }
    }
}
