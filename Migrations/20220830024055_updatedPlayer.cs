using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace the_greg_and_larry_show_api.Migrations
{
    public partial class updatedPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Players",
                newName: "Username");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Players",
                newName: "Name");
        }
    }
}
