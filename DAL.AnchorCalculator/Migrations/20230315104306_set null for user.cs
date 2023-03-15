using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.AnchorCalculator.Migrations
{
    /// <inheritdoc />
    public partial class setnullforuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors");

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors");

            migrationBuilder.AddForeignKey(
                name: "FK_Anchors_AspNetUsers_UserId",
                table: "Anchors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
