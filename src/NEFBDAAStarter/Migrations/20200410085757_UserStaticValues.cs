using Microsoft.EntityFrameworkCore.Migrations;

namespace NEFBDAAStarter.Migrations
{
    public partial class UserStaticValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserStaticoptions",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserTypeId",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserStaticoptions",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "Users");
        }
    }
}
