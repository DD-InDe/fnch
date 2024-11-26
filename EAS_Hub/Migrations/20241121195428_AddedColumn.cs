using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAS_Hub.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Source",
                table: "Module",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Source",
                table: "Module");
        }
    }
}
