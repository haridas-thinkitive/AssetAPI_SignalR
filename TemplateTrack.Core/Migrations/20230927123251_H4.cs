using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TemplateTrack.Core.Migrations
{
    /// <inheritdoc />
    public partial class H4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26a60970-5a1f-4e00-b984-3b2aac3bd1e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bab3e95-c3b9-4d8b-a7d7-1ea32793ba4e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "31daca02-8f14-459f-9e22-6d3e01072de5", "2", "Developer", "Developer" },
                    { "5ccece0b-5557-47da-be6e-990ae1049b00", "1", "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "31daca02-8f14-459f-9e22-6d3e01072de5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ccece0b-5557-47da-be6e-990ae1049b00");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26a60970-5a1f-4e00-b984-3b2aac3bd1e5", "1", "Admin", "Admin" },
                    { "8bab3e95-c3b9-4d8b-a7d7-1ea32793ba4e", "2", "Developer", "Developer" }
                });
        }
    }
}
