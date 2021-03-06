using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceQuationApi.Migrations
{
    public partial class _20210306Oliver1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bom_OPPO_OppoId",
                table: "Bom");

            migrationBuilder.RenameColumn(
                name: "OppoId",
                table: "Bom",
                newName: "OPPOId");

            migrationBuilder.RenameIndex(
                name: "IX_Bom_OppoId",
                table: "Bom",
                newName: "IX_Bom_OPPOId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bom_OPPO_OPPOId",
                table: "Bom",
                column: "OPPOId",
                principalTable: "OPPO",
                principalColumn: "OppoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bom_OPPO_OPPOId",
                table: "Bom");

            migrationBuilder.RenameColumn(
                name: "OPPOId",
                table: "Bom",
                newName: "OppoId");

            migrationBuilder.RenameIndex(
                name: "IX_Bom_OPPOId",
                table: "Bom",
                newName: "IX_Bom_OppoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bom_OPPO_OppoId",
                table: "Bom",
                column: "OppoId",
                principalTable: "OPPO",
                principalColumn: "OppoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
