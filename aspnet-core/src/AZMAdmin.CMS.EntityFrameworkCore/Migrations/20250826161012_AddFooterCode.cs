using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AZMAdmin.CMS.Migrations
{
    /// <inheritdoc />
    public partial class AddFooterCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Footers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Footers");
        }
    }
}
