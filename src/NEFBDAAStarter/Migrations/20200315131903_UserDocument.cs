using Microsoft.EntityFrameworkCore.Migrations;

namespace NEFBDAAStarter.Migrations
{
    public partial class UserDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserDocumentId",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDocumentId",
                table: "Users");
        }
    }
}
