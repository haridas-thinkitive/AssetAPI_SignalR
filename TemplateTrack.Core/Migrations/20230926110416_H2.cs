using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TemplateTrack.Core.Migrations
{
    /// <inheritdoc />
    public partial class H2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
