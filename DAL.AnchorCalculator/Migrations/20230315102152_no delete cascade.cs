using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    /// <inheritdoc />
    public partial class nodeletecascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors");

            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialId",
                table: "Anchors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors");

            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors");

            migrationBuilder.AlterColumn<int>(
                name: "MaterialId",
                table: "Anchors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_Materials_MaterialId",
                table: "Anchors",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
