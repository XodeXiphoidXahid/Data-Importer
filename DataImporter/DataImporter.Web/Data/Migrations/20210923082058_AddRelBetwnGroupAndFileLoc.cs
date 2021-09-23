using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Data.migrations
{
    public partial class AddRelBetwnGroupAndFileLoc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_FileLocations_GroupId",
                table: "FileLocations",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileLocations_Groups_GroupId",
                table: "FileLocations",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileLocations_Groups_GroupId",
                table: "FileLocations");

            migrationBuilder.DropIndex(
                name: "IX_FileLocations_GroupId",
                table: "FileLocations");
        }
    }
}
