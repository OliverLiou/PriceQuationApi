﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PriceQuationApi.Model;

namespace PriceQuationApi.Migrations
{
    [DbContext(typeof(PriceQuationContext))]
    partial class PriceQuationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("PriceQuationApi.Model.Bom", b =>
                {
                    b.Property<string>("AssemblyPartNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("AllFinishTime")
                        .HasColumnType("Date");

                    b.Property<string>("AssemblyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssemblyNameEng")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AssemblyRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("Date");

                    b.Property<string>("Customer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("Date");

                    b.Property<string>("OppoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AssemblyPartNumber");

                    b.HasIndex("OppoId");

                    b.ToTable("Bom");
                });

            modelBuilder.Entity("PriceQuationApi.Model.BomItem", b =>
                {
                    b.Property<string>("BomItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssemblyPartNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Material")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModelCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NeworOld")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OldCarType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartLevel")
                        .HasColumnType("int");

                    b.Property<string>("PartName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartName_Eng")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutingNo1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutingNo2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutingNo3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutingNo4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutingRule1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutingRule2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutingRule3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoutingRule4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThicknessWire")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BomItemId");

                    b.HasIndex("AssemblyPartNumber");

                    b.ToTable("BomItem");
                });

            modelBuilder.Entity("PriceQuationApi.Model.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");

                    b.HasData(
                        new
                        {
                            DepartmentId = 1,
                            Code = "HQ3200",
                            Name = "營業"
                        },
                        new
                        {
                            DepartmentId = 2,
                            Code = "HQ2110",
                            Name = "採購"
                        },
                        new
                        {
                            DepartmentId = 3,
                            Code = "HQ8100",
                            Name = "工機-模具"
                        },
                        new
                        {
                            DepartmentId = 4,
                            Code = "HQ8200",
                            Name = "工機-設備"
                        },
                        new
                        {
                            DepartmentId = 5,
                            Code = "HQ8140",
                            Name = "工機-量檢具"
                        },
                        new
                        {
                            DepartmentId = 6,
                            Code = "HQ8130",
                            Name = "工機-夾治具"
                        },
                        new
                        {
                            DepartmentId = 7,
                            Code = "HQ4100",
                            Name = "試驗課"
                        },
                        new
                        {
                            DepartmentId = 8,
                            Code = "HQ5100",
                            Name = "生管"
                        },
                        new
                        {
                            DepartmentId = 9,
                            Code = "HQ4000",
                            Name = "設計"
                        },
                        new
                        {
                            DepartmentId = 10,
                            Code = "HQ3330",
                            Name = "成本課"
                        },
                        new
                        {
                            DepartmentId = 11,
                            Code = "HQ4910",
                            Name = "ME"
                        });
                });

            modelBuilder.Entity("PriceQuationApi.Model.FixtureItem", b =>
                {
                    b.Property<string>("FixtureItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssemblyPartNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DepartemntId")
                        .HasColumnType("int");

                    b.Property<string>("EngineeringName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EngineeringOrder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EquipmentName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("EquipmentQuantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("EquipmentRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("EquipmentTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("EquipmentUnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FixtureName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("FixtureQuantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("FixtureRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("FixtureTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("FixtureUnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("NeedEquipment")
                        .HasColumnType("bit");

                    b.Property<bool>("NeedFixture")
                        .HasColumnType("bit");

                    b.Property<string>("PartNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Share")
                        .HasColumnType("bit");

                    b.HasKey("FixtureItemId");

                    b.HasIndex("AssemblyPartNumber");

                    b.ToTable("FixtureItem");
                });

            modelBuilder.Entity("PriceQuationApi.Model.MeasuringItem", b =>
                {
                    b.Property<string>("MeasuringItemId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AssemblyPartNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DepartemntId")
                        .HasColumnType("int");

                    b.Property<string>("MeasuringName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MeasuringRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MeasuringTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MeasuringTotalRemark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("MeasuringUnitFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("NeedMeausring")
                        .HasColumnType("bit");

                    b.Property<string>("PartNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("MeasuringItemId");

                    b.HasIndex("AssemblyPartNumber");

                    b.ToTable("MeasuringItem");
                });

            modelBuilder.Entity("PriceQuationApi.Model.OPPO", b =>
                {
                    b.Property<string>("OppoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("OppoId");

                    b.ToTable("OPPO");
                });

            modelBuilder.Entity("PriceQuationApi.Model.QuoteDetail", b =>
                {
                    b.Property<int>("QuoteDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("AssemblyPartNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("FinishedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("QuoteItemId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("QuoteTime")
                        .HasColumnType("Date");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("QuoteDetailId");

                    b.HasIndex("AssemblyPartNumber");

                    b.HasIndex("UserId");

                    b.ToTable("QuoteDetail");
                });

            modelBuilder.Entity("PriceQuationApi.Model.QuoteItem", b =>
                {
                    b.Property<int>("QuoteItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("DepartemntId")
                        .HasColumnType("int");

                    b.Property<string>("ResponsibleItem")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuoteItemId");

                    b.HasIndex("DepartemntId");

                    b.ToTable("QuoteItem");

                    b.HasData(
                        new
                        {
                            QuoteItemId = 1,
                            DepartemntId = 3,
                            ResponsibleItem = "自製件"
                        },
                        new
                        {
                            QuoteItemId = 2,
                            DepartemntId = 2,
                            ResponsibleItem = "外包件"
                        },
                        new
                        {
                            QuoteItemId = 3,
                            DepartemntId = 10,
                            ResponsibleItem = "延用件"
                        },
                        new
                        {
                            QuoteItemId = 4,
                            DepartemntId = 1,
                            ResponsibleItem = "進口件"
                        },
                        new
                        {
                            QuoteItemId = 5,
                            DepartemntId = 5,
                            ResponsibleItem = "量檢具費"
                        },
                        new
                        {
                            QuoteItemId = 6,
                            DepartemntId = 6,
                            ResponsibleItem = "夾治具費"
                        },
                        new
                        {
                            QuoteItemId = 7,
                            DepartemntId = 4,
                            ResponsibleItem = "設備費"
                        },
                        new
                        {
                            QuoteItemId = 8,
                            DepartemntId = 1,
                            ResponsibleItem = "總成組立費"
                        },
                        new
                        {
                            QuoteItemId = 9,
                            DepartemntId = 8,
                            ResponsibleItem = "包裝&運輸費"
                        },
                        new
                        {
                            QuoteItemId = 10,
                            DepartemntId = 2,
                            ResponsibleItem = "打樣費"
                        },
                        new
                        {
                            QuoteItemId = 11,
                            DepartemntId = 7,
                            ResponsibleItem = "試驗費"
                        });
                });

            modelBuilder.Entity("PriceQuationApi.Model.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Account")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<bool>("Alive")
                        .HasColumnType("bit");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PassWord")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("UserId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PriceQuationApi.Model.Bom", b =>
                {
                    b.HasOne("PriceQuationApi.Model.OPPO", "OPPO")
                        .WithMany("Boms")
                        .HasForeignKey("OppoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("OPPO");
                });

            modelBuilder.Entity("PriceQuationApi.Model.BomItem", b =>
                {
                    b.HasOne("PriceQuationApi.Model.Bom", "Bom")
                        .WithMany("BomItems")
                        .HasForeignKey("AssemblyPartNumber")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Bom");
                });

            modelBuilder.Entity("PriceQuationApi.Model.FixtureItem", b =>
                {
                    b.HasOne("PriceQuationApi.Model.Bom", "Bom")
                        .WithMany("FixtureItems")
                        .HasForeignKey("AssemblyPartNumber")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Bom");
                });

            modelBuilder.Entity("PriceQuationApi.Model.MeasuringItem", b =>
                {
                    b.HasOne("PriceQuationApi.Model.Bom", "Bom")
                        .WithMany("MeasuringItems")
                        .HasForeignKey("AssemblyPartNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bom");
                });

            modelBuilder.Entity("PriceQuationApi.Model.QuoteDetail", b =>
                {
                    b.HasOne("PriceQuationApi.Model.Bom", "Bom")
                        .WithMany("QuoteDetails")
                        .HasForeignKey("AssemblyPartNumber")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PriceQuationApi.Model.User", null)
                        .WithMany("QuoteDetails")
                        .HasForeignKey("UserId");

                    b.Navigation("Bom");
                });

            modelBuilder.Entity("PriceQuationApi.Model.QuoteItem", b =>
                {
                    b.HasOne("PriceQuationApi.Model.Department", "Department")
                        .WithMany("QuoteItems")
                        .HasForeignKey("DepartemntId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("PriceQuationApi.Model.User", b =>
                {
                    b.HasOne("PriceQuationApi.Model.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("PriceQuationApi.Model.Bom", b =>
                {
                    b.Navigation("BomItems");

                    b.Navigation("FixtureItems");

                    b.Navigation("MeasuringItems");

                    b.Navigation("QuoteDetails");
                });

            modelBuilder.Entity("PriceQuationApi.Model.Department", b =>
                {
                    b.Navigation("QuoteItems");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("PriceQuationApi.Model.OPPO", b =>
                {
                    b.Navigation("Boms");
                });

            modelBuilder.Entity("PriceQuationApi.Model.User", b =>
                {
                    b.Navigation("QuoteDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
