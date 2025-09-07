using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckContraint_MovieTable_ReleaseYear_And_Rating_Property : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CHK_RATING",
                table: "Movies",
                sql: "[Rating] >= 1 AND [Rating] <= 10");

            migrationBuilder.AddCheckConstraint(
                name: "CHK_RELEASEYEAR",
                table: "Movies",
                sql: "[ReleaseYear] >= 1000 AND [ReleaseYear] <= 2025");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CHK_RATING",
                table: "Movies");

            migrationBuilder.DropCheckConstraint(
                name: "CHK_RELEASEYEAR",
                table: "Movies");
        }
    }
}
