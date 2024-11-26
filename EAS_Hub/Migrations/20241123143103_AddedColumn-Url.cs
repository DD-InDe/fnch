using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAS_Hub.Migrations
{
    /// <inheritdoc />
    public partial class AddedColumnUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Material",
                type: "nvarchar(300)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Material");
        }
    }
}
