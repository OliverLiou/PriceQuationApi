using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceQuationApi.Migrations
{
    public partial class _20210304init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "OPPO",
                columns: table => new
                {
                    OppoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPPO", x => x.OppoId);
                });

            migrationBuilder.CreateTable(
                name: "QuoteItem",
                columns: table => new
                {
                    QuoteItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsibleItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartemntId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteItem", x => x.QuoteItemId);
                    table.ForeignKey(
                        name: "FK_QuoteItem_Department_DepartemntId",
                        column: x => x.DepartemntId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Account = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PassWord = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    Alive = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_User_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bom",
                columns: table => new
                {
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyNameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "Date", nullable: true),
                    AllFinishTime = table.Column<DateTime>(type: "Date", nullable: true),
                    OppoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bom", x => x.AssemblyPartNumber);
                    table.ForeignKey(
                        name: "FK_Bom_OPPO_OppoId",
                        column: x => x.OppoId,
                        principalTable: "OPPO",
                        principalColumn: "OppoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BomItem",
                columns: table => new
                {
                    BomItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PartLevel = table.Column<int>(type: "int", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    NeworOld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldCarType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BomItem", x => x.BomItemId);
                    table.ForeignKey(
                        name: "FK_BomItem_Bom_AssemblyPartNumber",
                        column: x => x.AssemblyPartNumber,
                        principalTable: "Bom",
                        principalColumn: "AssemblyPartNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FixtureItem",
                columns: table => new
                {
                    FixtureItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PartNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EngineeringName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineeringOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Share = table.Column<bool>(type: "bit", nullable: false),
                    NeedFixture = table.Column<bool>(type: "bit", nullable: false),
                    FixtureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixtureQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DepartemntId = table.Column<int>(type: "int", nullable: false),
                    FixtureRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixtureUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FixtureTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NeedEquipment = table.Column<bool>(type: "bit", nullable: false),
                    EquipmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EquipmentUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EquipmentTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EquipmentRemark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FixtureItem", x => x.FixtureItemId);
                    table.ForeignKey(
                        name: "FK_FixtureItem_Bom_AssemblyPartNumber",
                        column: x => x.AssemblyPartNumber,
                        principalTable: "Bom",
                        principalColumn: "AssemblyPartNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MeasuringItem",
                columns: table => new
                {
                    MeasuringItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NeedMeausring = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasuringName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasuringRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartemntId = table.Column<int>(type: "int", nullable: false),
                    MeasuringUnitFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasuringTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasuringTotalRemark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasuringItem", x => x.MeasuringItemId);
                    table.ForeignKey(
                        name: "FK_MeasuringItem_Bom_AssemblyPartNumber",
                        column: x => x.AssemblyPartNumber,
                        principalTable: "Bom",
                        principalColumn: "AssemblyPartNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuoteDetail",
                columns: table => new
                {
                    QuoteDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    QuoteItemId = table.Column<int>(type: "int", nullable: false),
                    QuoteTime = table.Column<DateTime>(type: "Date", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    FinishedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteDetail", x => x.QuoteDetailId);
                    table.ForeignKey(
                        name: "FK_QuoteDetail_Bom_AssemblyPartNumber",
                        column: x => x.AssemblyPartNumber,
                        principalTable: "Bom",
                        principalColumn: "AssemblyPartNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuoteDetail_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Department",
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
                table: "QuoteItem",
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
                name: "IX_Bom_OppoId",
                table: "Bom",
                column: "OppoId");

            migrationBuilder.CreateIndex(
                name: "IX_BomItem_AssemblyPartNumber_PartNumber",
                table: "BomItem",
                columns: new[] { "AssemblyPartNumber", "PartNumber" },
                unique: true,
                filter: "[AssemblyPartNumber] IS NOT NULL AND [PartNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FixtureItem_AssemblyPartNumber_PartNumber",
                table: "FixtureItem",
                columns: new[] { "AssemblyPartNumber", "PartNumber" },
                unique: true,
                filter: "[AssemblyPartNumber] IS NOT NULL AND [PartNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringItem_AssemblyPartNumber_PartNumber",
                table: "MeasuringItem",
                columns: new[] { "AssemblyPartNumber", "PartNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuoteDetail_AssemblyPartNumber",
                table: "QuoteDetail",
                column: "AssemblyPartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteDetail_UserId",
                table: "QuoteDetail",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteItem_DepartemntId",
                table: "QuoteItem",
                column: "DepartemntId");

            migrationBuilder.CreateIndex(
                name: "IX_User_DepartmentId",
                table: "User",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BomItem");

            migrationBuilder.DropTable(
                name: "FixtureItem");

            migrationBuilder.DropTable(
                name: "MeasuringItem");

            migrationBuilder.DropTable(
                name: "QuoteDetail");

            migrationBuilder.DropTable(
                name: "QuoteItem");

            migrationBuilder.DropTable(
                name: "Bom");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "OPPO");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
