using Microsoft.EntityFrameworkCore.Migrations;

namespace BolcherDBDataAccessLibrary.Migrations
{
    public partial class AddedNamePropertyToCandy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "productionCost",
                table: "Candy",
                newName: "ProductionCost");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Candy",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Candy");

            migrationBuilder.RenameColumn(
                name: "ProductionCost",
                table: "Candy",
                newName: "productionCost");
        }
    }
}
