using Microsoft.EntityFrameworkCore.Migrations;

namespace NEFBDAAStarter.Migrations
{
    public partial class LanguageCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OwnerCompanyId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_OwnerCompanyId",
                table: "Users",
                column: "OwnerCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Companies_OwnerCompanyId",
                table: "Users",
                column: "OwnerCompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Companies_OwnerCompanyId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_OwnerCompanyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OwnerCompanyId",
                table: "Users");
        }
    }
}
