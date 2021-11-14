using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations
{
    public partial class AddExportIdInHit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExportId",
                table: "ExportEmailHits",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExportId",
                table: "ExportEmailHits");
        }
    }
}
