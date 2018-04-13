using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CurrentLongitude",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<double>(
                name: "CurrentLatitude",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "CurrentLongitude",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "CurrentLatitude",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(double),
                oldNullable: true);
        }
    }
}
