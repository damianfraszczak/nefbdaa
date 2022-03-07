using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NEFBDAAStarter.Migrations
{
    public partial class Logs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hits",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUsedDate",
                table: "Documents",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "AppLogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hits",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "LastUsedDate",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "AppLogs");
        }
    }
}
