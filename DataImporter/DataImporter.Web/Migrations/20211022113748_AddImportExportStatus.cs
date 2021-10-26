﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations
{
    public partial class AddImportExportStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ImportHistories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "ExportHistories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ImportHistories");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ExportHistories");
        }
    }
}
