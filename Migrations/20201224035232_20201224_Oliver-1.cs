using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceQuationApi.Migrations
{
    public partial class _20201224_Oliver1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartemntId",
                table: "QuoteDetails",
                newName: "QuoteItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "QuoteItemId",
                table: "QuoteDetails",
                newName: "DepartemntId");
        }
    }
}
