using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TemplateTrack.Core.Migrations
{
    /// <inheritdoc />
    public partial class N1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state_district = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISO31662lvl4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country_code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrackingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    place_id = table.Column<int>(type: "int", nullable: false),
                    licence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    osm_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    osm_id = table.Column<int>(type: "int", nullable: false),
                    lat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    @class = table.Column<string>(name: "class", type: "nvarchar(max)", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    place_rank = table.Column<int>(type: "int", nullable: false),
                    importance = table.Column<double>(type: "float", nullable: false),
                    addresstype = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    display_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    addressId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackingDetails_Address_addressId",
                        column: x => x.addressId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackingDetails_addressId",
                table: "TrackingDetails",
                column: "addressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackingDetails");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
