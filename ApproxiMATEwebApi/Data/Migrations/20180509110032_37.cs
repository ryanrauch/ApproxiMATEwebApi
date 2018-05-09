using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class _37 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.AlterColumn<string>(
                name: "HistoryID",
                table: "LocationHistory",
                nullable: false,
                oldClrType: typeof(Guid));
                */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "HistoryID",
                table: "LocationHistory",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
