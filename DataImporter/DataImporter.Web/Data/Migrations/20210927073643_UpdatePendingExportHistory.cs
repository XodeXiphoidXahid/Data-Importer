using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Data.migrations
{
    public partial class UpdatePendingExportHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PendingExportHistories_GroupId",
                table: "PendingExportHistories",
                column: "GroupId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PendingExportHistories_Groups_GroupId",
                table: "PendingExportHistories",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PendingExportHistories_Groups_GroupId",
                table: "PendingExportHistories");

            migrationBuilder.DropIndex(
                name: "IX_PendingExportHistories_GroupId",
                table: "PendingExportHistories");
        }
    }
}
