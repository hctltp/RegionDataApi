using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegionDataApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RegionTypeCodeAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegionTypeCode",
                table: "Tbl_ProvienceRegionData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionTypeCode",
                table: "Tbl_ProvienceRegionData");
        }
    }
}
