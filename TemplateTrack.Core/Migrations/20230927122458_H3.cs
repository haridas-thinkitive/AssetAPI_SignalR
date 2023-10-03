using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TemplateTrack.Core.Migrations
{
    /// <inheritdoc />
    public partial class H3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6733eb5d-f5f2-4ce5-a7a2-e427359c6f67");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7861d501-78c0-4182-bab8-fee28727daa5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9836036-100b-45f9-9cc1-6d8b74cd8ef6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26a60970-5a1f-4e00-b984-3b2aac3bd1e5", "1", "Admin", "Admin" },
                    { "8bab3e95-c3b9-4d8b-a7d7-1ea32793ba4e", "2", "Developer", "Developer" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "6733eb5d-f5f2-4ce5-a7a2-e427359c6f67", "3", "Manager", "Manager" },
                    { "7861d501-78c0-4182-bab8-fee28727daa5", "2", "Developer", "Developer" },
                    { "b9836036-100b-45f9-9cc1-6d8b74cd8ef6", "1", "Admin", "Admin" }
                });
        }
    }
}
