using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    /// <inheritdoc />
    public partial class setnullformaterial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors");

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors");

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");
        }
    }
}
