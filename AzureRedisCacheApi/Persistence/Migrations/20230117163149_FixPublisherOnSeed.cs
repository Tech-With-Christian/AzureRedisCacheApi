using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AzureRedisCacheApi.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixPublisherOnSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Publisher",
                value: "Pearson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Publisher",
                value: "");
        }
    }
}
