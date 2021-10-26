﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace DataImporter.Web.Migrations
{
    public partial class AddFileNameInImportHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ImportHistories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ImportHistories");
        }
    }
}
