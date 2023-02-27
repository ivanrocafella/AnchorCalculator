using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    /// <inheritdoc />
    public partial class newfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "PricePerMetr",
                table: "Materials",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BatchWeight",
                table: "Anchors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "BilletLength",
                table: "Anchors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "SvgPath",
                table: "Anchors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "PricePerMetr",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "BatchWeight",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "BilletLength",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "SvgPath",
                table: "Anchors");
        }
    }
}
