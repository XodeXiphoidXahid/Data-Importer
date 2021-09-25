using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Data.migrations
{
    public partial class AddExportEmailHit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExportEmailHits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExportHit = table.Column<int>(type: "int", nullable: false),
                    EmailHit = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExportEmailHits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExportEmailHits_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExportEmailHits_GroupId",
                table: "ExportEmailHits",
                column: "GroupId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExportEmailHits");
        }
    }
}
