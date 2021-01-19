using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceQuationApi.Migrations
{
    public partial class _20210119_Oliver2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeasuringItem_Boms_BomAssemblyPartNumber",
                table: "MeasuringItem");

            migrationBuilder.DropIndex(
                name: "IX_MeasuringItem_BomAssemblyPartNumber",
                table: "MeasuringItem");

            migrationBuilder.DropColumn(
                name: "BomAssemblyPartNumber",
                table: "MeasuringItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BomAssemblyPartNumber",
                table: "MeasuringItem",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringItem_BomAssemblyPartNumber",
                table: "MeasuringItem",
                column: "BomAssemblyPartNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_MeasuringItem_Boms_BomAssemblyPartNumber",
                table: "MeasuringItem",
                column: "BomAssemblyPartNumber",
                principalTable: "Boms",
                principalColumn: "AssemblyPartNumber",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
