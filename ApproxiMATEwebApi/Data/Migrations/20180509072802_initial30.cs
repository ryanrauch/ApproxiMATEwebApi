using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_FriendRequests_AspNetUsers_InitiatorId1",
            //    table: "FriendRequests");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_FriendRequests_AspNetUsers_TargetId1",
            //    table: "FriendRequests");

            //migrationBuilder.DropIndex(
            //    name: "IX_FriendRequests_InitiatorId1",
            //    table: "FriendRequests");

            //migrationBuilder.DropIndex(
            //    name: "IX_FriendRequests_TargetId1",
            //    table: "FriendRequests");

            //migrationBuilder.DropColumn(
            //    name: "InitiatorId1",
            //    table: "FriendRequests");

            //migrationBuilder.DropColumn(
            //    name: "TargetId1",
            //    table: "FriendRequests");

            //migrationBuilder.AlterColumn<string>(
            //    name: "TargetId",
            //    table: "FriendRequests",
            //    nullable: false,
            //    oldClrType: typeof(Guid));

            //migrationBuilder.AlterColumn<string>(
            //    name: "InitiatorId",
            //    table: "FriendRequests",
            //    nullable: false,
            //    oldClrType: typeof(Guid));
            migrationBuilder.DropTable(
               name: "FriendRequests");

            migrationBuilder.CreateTable(
                name: "FriendRequests",
                columns: table => new
                {
                    InitiatorId = table.Column<string>(nullable: false),
                    TargetId = table.Column<string>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: true),
                    TargetViewed = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendRequests", x => new { x.InitiatorId, x.TargetId });
                });

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_TargetId",
                table: "FriendRequests",
                column: "TargetId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_InitiatorId",
                table: "FriendRequests",
                column: "InitiatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_TargetId",
                table: "FriendRequests",
                column: "TargetId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_InitiatorId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_TargetId",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_TargetId",
                table: "FriendRequests");

            migrationBuilder.AlterColumn<Guid>(
                name: "TargetId",
                table: "FriendRequests",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<Guid>(
                name: "InitiatorId",
                table: "FriendRequests",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "InitiatorId1",
                table: "FriendRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetId1",
                table: "FriendRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_InitiatorId1",
                table: "FriendRequests",
                column: "InitiatorId1");

            migrationBuilder.CreateIndex(
                name: "IX_FriendRequests_TargetId1",
                table: "FriendRequests",
                column: "TargetId1");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_InitiatorId1",
                table: "FriendRequests",
                column: "InitiatorId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_AspNetUsers_TargetId1",
                table: "FriendRequests",
                column: "TargetId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
