using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NEFBDAAStarter.Migrations
{
    public partial class MultipleRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_BaseUser_UserId",
                table: "CompanyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_BaseUser_OwnerId",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_OwnerId",
                table: "Reports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseUser",
                table: "BaseUser");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "BaseUser");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "BaseUser");

            migrationBuilder.RenameTable(
                name: "BaseUser",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_BaseUser_Email",
                table: "Users",
                newName: "IX_Users_Email");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerGuid",
                table: "Reports",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Roles",
                table: "Users",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_Users_UserId",
                table: "CompanyUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Users_UserId",
                table: "CompanyUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerGuid",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Roles",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "BaseUser");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Email",
                table: "BaseUser",
                newName: "IX_BaseUser_Email");

            migrationBuilder.AddColumn<long>(
                name: "OwnerId",
                table: "Reports",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "BaseUser",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "BaseUser",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "BaseUser",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseUser",
                table: "BaseUser",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_OwnerId",
                table: "Reports",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_BaseUser_UserId",
                table: "CompanyUser",
                column: "UserId",
                principalTable: "BaseUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_BaseUser_OwnerId",
                table: "Reports",
                column: "OwnerId",
                principalTable: "BaseUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
