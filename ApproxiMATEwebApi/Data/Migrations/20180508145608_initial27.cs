using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial27 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Layer1Latitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer1Longitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer2187Latitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer2187Longitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer243Latitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer243Longitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer27Latitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer27Longitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer3Latitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer3Longitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer729Latitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer729Longitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer81Latitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer81Longitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer9Latitude",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "Layer9Longitude",
                table: "CurrentLayers");

            migrationBuilder.AddColumn<string>(
                name: "LayersDelimited",
                table: "CurrentLayers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LayersDelimited",
                table: "CurrentLayers");

            migrationBuilder.AddColumn<double>(
                name: "Layer1Latitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer1Longitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer2187Latitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer2187Longitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer243Latitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer243Longitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer27Latitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer27Longitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer3Latitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer3Longitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer729Latitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer729Longitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer81Latitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer81Longitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer9Latitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Layer9Longitude",
                table: "CurrentLayers",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
