﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace NEFBDAAStarter.Migrations
{
    public partial class User2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FcmTokensString",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FcmTokensString",
                table: "Users");
        }
    }
}
