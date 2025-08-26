using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AZMAdmin.CMS.Migrations
{
    /// <inheritdoc />
    public partial class AddNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_HomeBanners_ImageId",
                table: "HomeBanners",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_HomeBanners_LogoId",
                table: "HomeBanners",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_AttachmentId",
                table: "Contents",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ContentCategoryId",
                table: "Contents",
                column: "ContentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Attachments_AttachmentId",
                table: "Contents",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_ContentCategories_ContentCategoryId",
                table: "Contents",
                column: "ContentCategoryId",
                principalTable: "ContentCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeBanners_Attachments_ImageId",
                table: "HomeBanners",
                column: "ImageId",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeBanners_Attachments_LogoId",
                table: "HomeBanners",
                column: "LogoId",
                principalTable: "Attachments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Attachments_AttachmentId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_ContentCategories_ContentCategoryId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeBanners_Attachments_ImageId",
                table: "HomeBanners");

            migrationBuilder.DropForeignKey(
                name: "FK_HomeBanners_Attachments_LogoId",
                table: "HomeBanners");

            migrationBuilder.DropIndex(
                name: "IX_HomeBanners_ImageId",
                table: "HomeBanners");

            migrationBuilder.DropIndex(
                name: "IX_HomeBanners_LogoId",
                table: "HomeBanners");

            migrationBuilder.DropIndex(
                name: "IX_Contents_AttachmentId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ContentCategoryId",
                table: "Contents");
        }
    }
}
