using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateTrack.Core.Migrations
{
    /// <inheritdoc />
    public partial class seven : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "assetTrackingInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "assetTrackingInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "assetTrackingInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "assetTrackingInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "assetTrackingInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "landMark",
                table: "assetTrackingInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "assetTrackingInfos");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "assetTrackingInfos");

            migrationBuilder.DropColumn(
                name: "District",
                table: "assetTrackingInfos");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "assetTrackingInfos");

            migrationBuilder.DropColumn(
                name: "State",
                table: "assetTrackingInfos");

            migrationBuilder.DropColumn(
                name: "landMark",
                table: "assetTrackingInfos");
        }
    }
}
