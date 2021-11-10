using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations
{
    public partial class AddExportIdInPendingExportHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExportId",
                table: "PendingExportHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExportId",
                table: "PendingExportHistories");
        }
    }
}
