using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class _33 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentLayers_AspNetUsers_UserId1",
                table: "CurrentLayers");

            migrationBuilder.DropIndex(
                name: "IX_CurrentLayers_UserId1",
                table: "CurrentLayers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CurrentLayers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CurrentLayers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CurrentLayers_UserId1",
                table: "CurrentLayers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentLayers_AspNetUsers_UserId1",
                table: "CurrentLayers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
