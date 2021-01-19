using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceQuationApi.Migrations
{
    public partial class _20210119_Oliver1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MeasuringItem",
                columns: table => new
                {
                    BomItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NeedMeausring = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasuringName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasuringRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartemntId = table.Column<int>(type: "int", nullable: false),
                    MeasuringUnitFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasuringTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasuringTotalRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BomAssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuringItem", x => x.BomItemId);
                    table.ForeignKey(
                        name: "FK_MeasuringItem_BomItems_BomItemId",
                        column: x => x.BomItemId,
                        principalTable: "BomItems",
                        principalColumn: "No",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeasuringItem_Boms_BomAssemblyPartNumber",
                        column: x => x.BomAssemblyPartNumber,
                        principalTable: "Boms",
                        principalColumn: "AssemblyPartNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BomItems_No",
                table: "BomItems",
                column: "No",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringItem_BomAssemblyPartNumber",
                table: "MeasuringItem",
                column: "BomAssemblyPartNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeasuringItem");

            migrationBuilder.DropIndex(
                name: "IX_BomItems_No",
                table: "BomItems");
        }
    }
}
