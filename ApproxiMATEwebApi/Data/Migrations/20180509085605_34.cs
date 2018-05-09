using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class _34 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationHistories_AspNetUsers_UserId",
                table: "LocationHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationHistories",
                table: "LocationHistories");

            migrationBuilder.RenameTable(
                name: "LocationHistories",
                newName: "LocationHistory");

            migrationBuilder.RenameIndex(
                name: "IX_LocationHistories_UserId",
                table: "LocationHistory",
                newName: "IX_LocationHistory_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationHistory",
                table: "LocationHistory",
                column: "HistoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationHistory_AspNetUsers_UserId",
                table: "LocationHistory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LocationHistory_AspNetUsers_UserId",
                table: "LocationHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LocationHistory",
                table: "LocationHistory");

            migrationBuilder.RenameTable(
                name: "LocationHistory",
                newName: "LocationHistories");

            migrationBuilder.RenameIndex(
                name: "IX_LocationHistory_UserId",
                table: "LocationHistories",
                newName: "IX_LocationHistories_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LocationHistories",
                table: "LocationHistories",
                column: "HistoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_LocationHistories_AspNetUsers_UserId",
                table: "LocationHistories",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
