using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    /// <inheritdoc />
    public partial class fieldUserIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors");

            migrationBuilder.RenameColumn(
                name: "WeightAnchor",
                table: "Anchors",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "StepThread",
                table: "Anchors",
                newName: "ThreadStep");

            migrationBuilder.RenameColumn(
                name: "RadiusBend",
                table: "Anchors",
                newName: "ThreadLength");

            migrationBuilder.RenameColumn(
                name: "PriceTotalAnchor",
                table: "Anchors",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PriceAnchor",
                table: "Anchors",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "LengthThread",
                table: "Anchors",
                newName: "ThreadDiameter");

            migrationBuilder.RenameColumn(
                name: "LengthBend",
                table: "Anchors",
                newName: "Length");

            migrationBuilder.RenameColumn(
                name: "LengthAnchor",
                table: "Anchors",
                newName: "Diameter");

            migrationBuilder.RenameColumn(
                name: "DiameterThread",
                table: "Anchors",
                newName: "BendRadius");

            migrationBuilder.RenameColumn(
                name: "DiameterAnchor",
                table: "Anchors",
                newName: "BendLength");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Anchors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "Anchors",
                newName: "WeightAnchor");

            migrationBuilder.RenameColumn(
                name: "ThreadStep",
                table: "Anchors",
                newName: "StepThread");

            migrationBuilder.RenameColumn(
                name: "ThreadLength",
                table: "Anchors",
                newName: "RadiusBend");

            migrationBuilder.RenameColumn(
                name: "ThreadDiameter",
                table: "Anchors",
                newName: "LengthThread");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Anchors",
                newName: "PriceTotalAnchor");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Anchors",
                newName: "LengthBend");

            migrationBuilder.RenameColumn(
                name: "Diameter",
                table: "Anchors",
                newName: "LengthAnchor");

            migrationBuilder.RenameColumn(
                name: "BendRadius",
                table: "Anchors",
                newName: "DiameterThread");

            migrationBuilder.RenameColumn(
                name: "BendLength",
                table: "Anchors",
                newName: "DiameterAnchor");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Anchors",
                newName: "PriceAnchor");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Anchors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
