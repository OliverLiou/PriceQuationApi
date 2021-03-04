using Microsoft.EntityFrameworkCore;

namespace PriceQuationApi.Model
{
    public class PriceQuationContext : DbContext
    {
        public PriceQuationContext(DbContextOptions<PriceQuationContext> options)
        : base(options)
        {

        }
        public DbSet<OPPO> OPPO { get; set; }
        public DbSet<Bom> Bom { get; set; }
        public DbSet<QuoteDetail> QuoteDetail { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<User> User { get; set; }

        public DbSet<BomItem> BomItem { get; set; }
        public DbSet<MeasuringItem> MeasuringItem { get; set; }
        public DbSet<FixtureItem> FixtureItem { get; set; }
        public DbSet<QuoteItem> QuoteItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OPPO>()
                .HasKey( o => o.OppoId);

            modelBuilder.Entity<Bom>()
                .HasKey(b => b.BomId);

            modelBuilder.Entity<Bom>()
                .HasKey(b => b.AssemblyPartNumber);

            modelBuilder.Entity<BomItem>()
                .HasKey(b => b.BomItemId);

            modelBuilder.Entity<BomItem>()
                .HasIndex(b => new { b.AssemblyPartNumber, b.PartNumber }).IsUnique();

            modelBuilder.Entity<MeasuringItem>()
                .HasKey(m => m.MeasuringItemId);

            modelBuilder.Entity<MeasuringItem>()
                .HasIndex(m => new { m.AssemblyPartNumber, m.PartNumber }).IsUnique();

            modelBuilder.Entity<FixtureItem>()
                .HasKey(f => f.FixtureItemId);

            modelBuilder.Entity<FixtureItem>()
                .HasIndex(f => new { f.AssemblyPartNumber, f.PartNumber }).IsUnique();

            modelBuilder.Entity<QuoteDetail>()
               .HasKey(q => q.QuoteDetailId);

            modelBuilder.Entity<QuoteItem>()
               .HasKey(q => q.QuoteItemId);

            modelBuilder.Entity<Department>()
               .HasKey(d => d.DepartmentId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Bom>()
                .HasOne(b => b.OPPO)
                .WithMany(b => b.Boms)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BomItem>()
                .HasOne(b => b.Bom)
                .WithMany(b => b.BomItems)
                .HasForeignKey(b => b.AssemblyPartNumber)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MeasuringItem>()
                .HasOne(b => b.Bom)
                .WithMany(b => b.MeasuringItems)
                .HasForeignKey(b => b.AssemblyPartNumber)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FixtureItem>()
                .HasOne(b => b.Bom)
                .WithMany(b => b.FixtureItems)
                .HasForeignKey(b => b.AssemblyPartNumber)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuoteDetail>()
                .HasOne(q => q.Bom)
                .WithMany(q => q.QuoteDetails)
                .HasForeignKey(q => q.AssemblyPartNumber)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Department)
                .WithMany(q => q.Users)
                .HasForeignKey(q => q.DepartmentId);

            modelBuilder.Entity<QuoteItem>()
                .HasOne(q => q.Department)
                .WithMany(d => d.QuoteItems)
                .HasForeignKey(q => q.DepartemntId);

            modelBuilder.Entity<Department>()
             .HasData
             (
                 new Department() { DepartmentId = 1, Code = "HQ3200", Name = "營業" },
                 new Department() { DepartmentId = 2, Code = "HQ2110", Name = "採購" },
                 new Department() { DepartmentId = 3, Code = "HQ8100", Name = "工機-模具" },
                 new Department() { DepartmentId = 4, Code = "HQ8200", Name = "工機-設備" },
                 new Department() { DepartmentId = 5, Code = "HQ8140", Name = "工機-量檢具" },
                 new Department() { DepartmentId = 6, Code = "HQ8130", Name = "工機-夾治具" },
                 new Department() { DepartmentId = 7, Code = "HQ4100", Name = "試驗課" },
                 new Department() { DepartmentId = 8, Code = "HQ5100", Name = "生管" },
                 new Department() { DepartmentId = 9, Code = "HQ4000", Name = "設計" },
                 new Department() { DepartmentId = 10, Code = "HQ3330", Name = "成本課" },
                 new Department() { DepartmentId = 11, Code = "HQ4910", Name = "ME" }
             );

            modelBuilder.Entity<QuoteItem>()
            .HasData
            (
                new QuoteItem() { QuoteItemId = 1, ResponsibleItem = "自製件", DepartemntId = 3 },
                new QuoteItem() { QuoteItemId = 2, ResponsibleItem = "外包件", DepartemntId = 2 },
                new QuoteItem() { QuoteItemId = 3, ResponsibleItem = "延用件", DepartemntId = 10 },
                new QuoteItem() { QuoteItemId = 4, ResponsibleItem = "進口件", DepartemntId = 1 },
                new QuoteItem() { QuoteItemId = 5, ResponsibleItem = "量檢具費", DepartemntId = 5 },
                new QuoteItem() { QuoteItemId = 6, ResponsibleItem = "夾治具費", DepartemntId = 6 },
                new QuoteItem() { QuoteItemId = 7, ResponsibleItem = "設備費", DepartemntId = 4 },
                new QuoteItem() { QuoteItemId = 8, ResponsibleItem = "總成組立費", DepartemntId = 1 },
                new QuoteItem() { QuoteItemId = 9, ResponsibleItem = "包裝&運輸費", DepartemntId = 8 },
                new QuoteItem() { QuoteItemId = 10, ResponsibleItem = "打樣費", DepartemntId = 2 },
                new QuoteItem() { QuoteItemId = 11, ResponsibleItem = "試驗費", DepartemntId = 7 }
            );
        }
    }
}