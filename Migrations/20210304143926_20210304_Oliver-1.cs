using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceQuationApi.Migrations
{
    public partial class _20210304_Oliver1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MeasuringItem_AssemblyPartNumber_PartNumber",
                table: "MeasuringItem");

            migrationBuilder.DropIndex(
                name: "IX_FixtureItem_AssemblyPartNumber_PartNumber",
                table: "FixtureItem");

            migrationBuilder.DropIndex(
                name: "IX_BomItem_AssemblyPartNumber_PartNumber",
                table: "BomItem");

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                table: "MeasuringItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                table: "FixtureItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                table: "BomItem",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringItem_AssemblyPartNumber",
                table: "MeasuringItem",
                column: "AssemblyPartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FixtureItem_AssemblyPartNumber",
                table: "FixtureItem",
                column: "AssemblyPartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_BomItem_AssemblyPartNumber",
                table: "BomItem",
                column: "AssemblyPartNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MeasuringItem_AssemblyPartNumber",
                table: "MeasuringItem");

            migrationBuilder.DropIndex(
                name: "IX_FixtureItem_AssemblyPartNumber",
                table: "FixtureItem");

            migrationBuilder.DropIndex(
                name: "IX_BomItem_AssemblyPartNumber",
                table: "BomItem");

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                table: "MeasuringItem",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                table: "FixtureItem",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PartNumber",
                table: "BomItem",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringItem_AssemblyPartNumber_PartNumber",
                table: "MeasuringItem",
                columns: new[] { "AssemblyPartNumber", "PartNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FixtureItem_AssemblyPartNumber_PartNumber",
                table: "FixtureItem",
                columns: new[] { "AssemblyPartNumber", "PartNumber" },
                unique: true,
                filter: "[AssemblyPartNumber] IS NOT NULL AND [PartNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BomItem_AssemblyPartNumber_PartNumber",
                table: "BomItem",
                columns: new[] { "AssemblyPartNumber", "PartNumber" },
                unique: true,
                filter: "[AssemblyPartNumber] IS NOT NULL AND [PartNumber] IS NOT NULL");
        }
    }
}
