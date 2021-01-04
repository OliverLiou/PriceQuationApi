using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceQuationApi.Migrations
{
    public partial class _20201224init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boms",
                columns: table => new
                {
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssemblyNameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssemblyRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "Date", nullable: true),
                    AllFinishTime = table.Column<DateTime>(type: "Date", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boms", x => x.AssemblyPartNumber);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "BomItems",
                columns: table => new
                {
                    No = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartLevel = table.Column<int>(type: "int", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PartName_Eng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Material = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThicknessWire = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingNo1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingRule1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingNo2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingRule2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingNo3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingRule3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingNo4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutingRule4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NeworOld = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OldCarType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BomItems", x => x.No);
                    table.ForeignKey(
                        name: "FK_BomItems_Boms_AssemblyPartNumber",
                        column: x => x.AssemblyPartNumber,
                        principalTable: "Boms",
                        principalColumn: "AssemblyPartNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuoteItems",
                columns: table => new
                {
                    QuoteItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsibleItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartemntId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteItems", x => x.QuoteItemId);
                    table.ForeignKey(
                        name: "FK_QuoteItems_Departments_DepartemntId",
                        column: x => x.DepartemntId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Account = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PassWord = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Alive = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuoteDetails",
                columns: table => new
                {
                    QuoteDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartemntId = table.Column<int>(type: "int", nullable: false),
                    QuoteTime = table.Column<DateTime>(type: "Date", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    FinishedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteDetails", x => x.QuoteDetailId);
                    table.ForeignKey(
                        name: "FK_QuoteDetails_Boms_AssemblyPartNumber",
                        column: x => x.AssemblyPartNumber,
                        principalTable: "Boms",
                        principalColumn: "AssemblyPartNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "HQ3200", "營業" },
                    { 2, "HQ2110", "採購" },
                    { 3, "HQ8100", "工機-模具" },
                    { 4, "HQ8200", "工機-設備" },
                    { 5, "HQ8140", "工機-量檢具" },
                    { 6, "HQ8130", "工機-夾治具" },
                    { 7, "HQ4100", "試驗課" },
                    { 8, "HQ5100", "生管" },
                    { 9, "HQ4000", "設計" },
                    { 10, "HQ3330", "成本課" },
                    { 11, "HQ4910", "ME" }
                });

            migrationBuilder.InsertData(
                table: "QuoteItems",
                columns: new[] { "QuoteItemId", "DepartemntId", "ResponsibleItem" },
                values: new object[,]
                {
                    { 4, 1, "進口件" },
                    { 8, 1, "總成組立費" },
                    { 2, 2, "外包件" },
                    { 10, 2, "打樣費" },
                    { 1, 3, "自製件" },
                    { 7, 4, "設備費" },
                    { 5, 5, "量檢具費" },
                    { 6, 6, "夾治具費" },
                    { 11, 7, "試驗費" },
                    { 9, 8, "包裝&運輸費" },
                    { 3, 10, "延用件" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BomItems_AssemblyPartNumber",
                table: "BomItems",
                column: "AssemblyPartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteDetails_AssemblyPartNumber",
                table: "QuoteDetails",
                column: "AssemblyPartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteDetails_UserId",
                table: "QuoteDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteItems_DepartemntId",
                table: "QuoteItems",
                column: "DepartemntId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BomItems");

            migrationBuilder.DropTable(
                name: "QuoteDetails");

            migrationBuilder.DropTable(
                name: "QuoteItems");

            migrationBuilder.DropTable(
                name: "Boms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
