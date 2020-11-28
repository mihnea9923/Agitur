using Microsoft.EntityFrameworkCore.Migrations;

namespace Agitur.EFDataAccess.Migrations
{
    public partial class groupsnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupsNumber",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupsNumber",
                table: "Users");
        }
    }
}
