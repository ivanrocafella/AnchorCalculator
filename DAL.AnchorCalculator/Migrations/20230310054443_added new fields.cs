using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    /// <inheritdoc />
    public partial class addednewfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SvgPath",
                table: "Anchors",
                newName: "SvgElement");

            migrationBuilder.AlterColumn<float>(
                name: "Diameter",
                table: "Anchors",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SvgElement",
                table: "Anchors",
                newName: "SvgPath");

            migrationBuilder.AlterColumn<int>(
                name: "Diameter",
                table: "Anchors",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
