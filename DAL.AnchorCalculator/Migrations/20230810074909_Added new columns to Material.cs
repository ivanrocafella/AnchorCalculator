using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    public partial class AddednewcolumnstoMaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeTheradRolling",
                table: "Materials",
                newName: "TimeThreadRolling");

            migrationBuilder.AddColumn<double>(
                name: "Cutter",
                table: "Materials",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Plashka",
                table: "Materials",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TimeThreadCutting",
                table: "Materials",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cutter",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Plashka",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "TimeThreadCutting",
                table: "Materials");

            migrationBuilder.RenameColumn(
                name: "TimeThreadRolling",
                table: "Materials",
                newName: "TimeTheradRolling");
        }
    }
}
