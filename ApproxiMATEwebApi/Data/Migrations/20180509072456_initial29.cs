using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ApproxiMATEwebApi.Data.Migrations
{
    public partial class initial29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_InitiatorId1",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_AspNetUsers_TargetId1",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_InitiatorId1",
                table: "FriendRequests");

            migrationBuilder.DropIndex(
                name: "IX_FriendRequests_TargetId1",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "InitiatorId1",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "TargetId1",
                table: "FriendRequests");
        }
    }
}
