using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiameterAnchor",
                table: "Anchors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "Anchors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "PriceAnchor",
                table: "Anchors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PriceTotalAnchor",
                table: "Anchors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Anchors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RadiusBend",
                table: "Anchors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "StepThread",
                table: "Anchors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "WeightAnchor",
                table: "Anchors",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Anchors_MaterialId",
                table: "Anchors",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors");

            migrationBuilder.DropIndex(
                name: "IX_Anchors_MaterialId",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "DiameterAnchor",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "PriceAnchor",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "PriceTotalAnchor",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "RadiusBend",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "StepThread",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "WeightAnchor",
                table: "Anchors");
        }
    }
}
