using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegionDataApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class TableNameChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbl_ProvienceRegionData",
                table: "Tbl_ProvienceRegionData");

            migrationBuilder.RenameTable(
                name: "Tbl_ProvienceRegionData",
                newName: "Tbl_RegionData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbl_RegionData",
                table: "Tbl_RegionData",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tbl_RegionData",
                table: "Tbl_RegionData");

            migrationBuilder.RenameTable(
                name: "Tbl_RegionData",
                newName: "Tbl_ProvienceRegionData");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tbl_ProvienceRegionData",
                table: "Tbl_ProvienceRegionData",
                column: "Id");
        }
    }
}
