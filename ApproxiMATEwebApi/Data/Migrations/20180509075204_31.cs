using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class _31 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "CurrentLayers");

            migrationBuilder.CreateTable(
                name: "CurrentLayers",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LayersDelimited = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentLayers", x => x.UserId);
                });
            //migrationBuilder.AlterColumn<string>(
            //    name: "UserId",
            //    table: "CurrentLayers",
            //    nullable: false,
            //    oldClrType: typeof(Guid));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "CurrentLayers",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
