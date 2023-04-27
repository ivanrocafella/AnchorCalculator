using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    public partial class addedfieldstomaterial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "LengthBladeBandSaw",
                table: "Materials",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TimeBandSaw",
                table: "Materials",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TimeTheradRolling",
                table: "Materials",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LengthBladeBandSaw",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "TimeBandSaw",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "TimeTheradRolling",
                table: "Materials");
        }
    }
}
