using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AZMAdmin.CMS.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RedirectUrl",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedirectUrl",
                table: "Contents");
        }
    }
}
