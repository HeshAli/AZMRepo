using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AZMAdmin.CMS.Migrations
{
    /// <inheritdoc />
    public partial class AddEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Footers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ContentCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Footers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ContentCategories");
        }
    }
}
