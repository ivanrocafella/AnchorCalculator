using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationOneToManyUserAnchor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Anchors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Anchors_UserId",
                table: "Anchors",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_Users_UserId",
                table: "Anchors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_Users_UserId",
                table: "Anchors");

            migrationBuilder.DropIndex(
                name: "IX_Anchors_UserId",
                table: "Anchors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Anchors");
        }
    }
}
