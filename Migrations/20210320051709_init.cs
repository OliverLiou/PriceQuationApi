﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PriceQuationApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminRole",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Oppo",
                columns: table => new
                {
                    OppoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oppo", x => x.OppoId);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaim_AdminRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AdminRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdminUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminUser_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QuoteItem",
                columns: table => new
                {
                    QuoteItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResponsibleItem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartemntId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "Bom",
                columns: table => new
                {
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OppoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssemblyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyNameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Date", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "Date", nullable: true),
                    AllFinishTime = table.Column<DateTime>(type: "Date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bom", x => x.AssemblyPartNumber);
                    table.ForeignKey(
                        name: "FK_Bom_Oppo_OppoId",
                        column: x => x.OppoId,
                        principalTable: "Oppo",
                        principalColumn: "OppoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaim_AdminUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AdminUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_AdminUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AdminUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_AdminRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AdminRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_AdminUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AdminUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserToken_AdminUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AdminUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BomItem",
                columns: table => new
                {
                    BomItemId = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EngineeringName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineeringOrder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Share = table.Column<bool>(type: "bit", nullable: true),
                    NeedFixture = table.Column<bool>(type: "bit", nullable: true),
                    FixtureName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixtureQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DepartemntId = table.Column<int>(type: "int", nullable: true),
                    FixtureRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FixtureUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FixtureTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NeedEquipment = table.Column<bool>(type: "bit", nullable: true),
                    EquipmentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EquipmentQuantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EquipmentUnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    EquipmentTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    PartNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeedMeausring = table.Column<bool>(type: "bit", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MeasuringName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasuringRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartemntId = table.Column<int>(type: "int", nullable: false),
                    MeasuringUnitFee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MeasuringTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    AssemblyPartNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                });

            migrationBuilder.InsertData(
                table: "AdminRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "RoleDesc" },
                values: new object[,]
                {
                    { "SuperAdmin", "ConcurrencyStamp", "SuperAdmin", "SUPERADMIN", "超級管理員" },
                    { "Admin", "ConcurrencyStamp", "Admin", "ADMIN", "系統管理員" }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentId", "Code" },
                values: new object[,]
                {
                    { "營業", "HQ3200" },
                    { "採購", "HQ2110" },
                    { "工機-模具", "HQ8100" },
                    { "工機-設備", "HQ8200" },
                    { "工機-量檢具", "HQ8140" },
                    { "工機-夾治具", "HQ8130" },
                    { "試驗課", "HQ4100" },
                    { "生管", "HQ5100" },
                    { "設計", "HQ4000" },
                    { "成本課", "HQ3330" },
                    { "ME", "HQ4910" }
                });

            migrationBuilder.InsertData(
                table: "AdminUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DepartmentId", "Email", "EmailConfirmed", "EmployeeId", "EmployeeName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "sadmin", 0, "ConcurrencyStamp", "營業", "sadmin@hcmfgroup.com", false, null, null, false, null, "SADMIN@HCMFGROUP.COM", "SADMIN", "AQAAAAEAACcQAAAAEOtRfNDmY3fKqd9iqJINpOVUiLz8JFKzKEz/Xt46A/eIfMdpdMjueu4xYIYFRncnXg==", null, false, "SecurityStamp", false, "sadmin" });

            migrationBuilder.InsertData(
                table: "QuoteItem",
                columns: new[] { "QuoteItemId", "DepartemntId", "ResponsibleItem" },
                values: new object[,]
                {
                    { 4, "營業", "進口件" },
                    { 8, "營業", "總成組立費" },
                    { 2, "採購", "外包件" },
                    { 10, "採購", "打樣費" },
                    { 1, "工機-模具", "自製件" },
                    { 7, "工機-設備", "設備費" },
                    { 5, "工機-量檢具", "量檢具費" },
                    { 6, "工機-夾治具", "夾治具費" },
                    { 11, "試驗課", "試驗費" },
                    { 9, "生管", "包裝&運輸費" },
                    { 3, "成本課", "延用件" }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "SuperAdmin", "sadmin" });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AdminRole",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AdminUser",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUser_DepartmentId",
                table: "AdminUser",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AdminUser",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bom_OppoId",
                table: "Bom",
                column: "OppoId");

            migrationBuilder.CreateIndex(
                name: "IX_BomItem_AssemblyPartNumber",
                table: "BomItem",
                column: "AssemblyPartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_FixtureItem_AssemblyPartNumber",
                table: "FixtureItem",
                column: "AssemblyPartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_MeasuringItem_AssemblyPartNumber",
                table: "MeasuringItem",
                column: "AssemblyPartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteDetail_AssemblyPartNumber",
                table: "QuoteDetail",
                column: "AssemblyPartNumber");

            migrationBuilder.CreateIndex(
                name: "IX_QuoteItem_DepartemntId",
                table: "QuoteItem",
                column: "DepartemntId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaim_RoleId",
                table: "RoleClaim",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaim_UserId",
                table: "UserClaim",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
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
                name: "RoleClaim");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Bom");

            migrationBuilder.DropTable(
                name: "AdminRole");

            migrationBuilder.DropTable(
                name: "AdminUser");

            migrationBuilder.DropTable(
                name: "Oppo");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
