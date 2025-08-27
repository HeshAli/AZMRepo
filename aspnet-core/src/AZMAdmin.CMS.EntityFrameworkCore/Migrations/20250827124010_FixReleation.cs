using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AZMAdmin.CMS.Migrations
{
    /// <inheritdoc />
    public partial class FixReleation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CoursesDetails_CourseId",
                table: "CoursesDetails",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursesDetails_Courses_CourseId",
                table: "CoursesDetails",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursesDetails_Courses_CourseId",
                table: "CoursesDetails");

            migrationBuilder.DropIndex(
                name: "IX_CoursesDetails_CourseId",
                table: "CoursesDetails");
        }
    }
}
