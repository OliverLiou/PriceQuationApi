using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PriceQuationApi.Model
{
    public class PriceQuationContext : IdentityDbContext<AdminUser, AdminRole, string>
    {
        private IPasswordHasher<AdminUser> _passwordHasher;

        public PriceQuationContext(DbContextOptions<PriceQuationContext> options, IPasswordHasher<AdminUser> passwordHasher)
        : base(options)
        {
            _passwordHasher = passwordHasher;
        }

        public DbSet<Oppo> Oppo { get; set; }
        public DbSet<Bom> Bom { get; set; }
        public DbSet<QuoteDetail> QuoteDetail { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<AdminUser> AdminUser { get; set; }

        public DbSet<BomItem> BomItem { get; set; }
        public DbSet<MeasuringItem> MeasuringItem { get; set; }
        public DbSet<FixtureItem> FixtureItem { get; set; }
        public DbSet<QuoteItem> QuoteItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Identity
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AdminUser>().ToTable("AdminUser");
            modelBuilder.Entity<AdminRole>().ToTable("AdminRole");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");

            modelBuilder.Entity<Oppo>()
                .HasKey(o => o.OppoId);

            modelBuilder.Entity<Bom>()
                .HasKey(b => b.BomId);

            modelBuilder.Entity<Bom>()
                .HasKey(b => b.AssemblyPartNumber);

            modelBuilder.Entity<BomItem>()
                .HasKey(b => b.BomItemId);

            modelBuilder.Entity<MeasuringItem>()
                .HasKey(m => m.MeasuringItemId);

            modelBuilder.Entity<FixtureItem>()
                .HasKey(f => f.FixtureItemId);

            modelBuilder.Entity<QuoteDetail>()
               .HasKey(q => q.QuoteDetailId);

            modelBuilder.Entity<QuoteItem>()
               .HasKey(q => q.QuoteItemId);

            modelBuilder.Entity<Department>()
               .HasKey(d => d.DepartmentId);

            // modelBuilder.Entity<AdminUser>()
            //     .HasKey(u => u.UserId);

            modelBuilder.Entity<Bom>()
                .HasOne(b => b.Oppo)
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

            modelBuilder.Entity<AdminUser>()
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
                 new Department() { DepartmentId = "營業" , Code = "HQ3200" },
                 new Department() { DepartmentId = "採購", Code = "HQ2110" },
                 new Department() { DepartmentId = "工機-模具", Code = "HQ8100" },
                 new Department() { DepartmentId = "工機-設備", Code = "HQ8200" },
                 new Department() { DepartmentId = "工機-量檢具", Code = "HQ8140" },
                 new Department() { DepartmentId = "工機-夾治具", Code = "HQ8130" },
                 new Department() { DepartmentId = "試驗課", Code = "HQ4100" },
                 new Department() { DepartmentId = "生管", Code = "HQ5100" },
                 new Department() { DepartmentId = "設計", Code = "HQ4000" },
                 new Department() { DepartmentId = "成本課", Code = "HQ3330" },
                 new Department() { DepartmentId = "ME", Code = "HQ4910" }
             );

            modelBuilder.Entity<QuoteItem>()
            .HasData
            (
                new QuoteItem() { QuoteItemId = 1, ResponsibleItem = "自製件", DepartemntId = "工機-模具" },
                new QuoteItem() { QuoteItemId = 2, ResponsibleItem = "外包件", DepartemntId = "採購" },
                new QuoteItem() { QuoteItemId = 3, ResponsibleItem = "延用件", DepartemntId = "成本課" },
                new QuoteItem() { QuoteItemId = 4, ResponsibleItem = "進口件", DepartemntId = "營業" },
                new QuoteItem() { QuoteItemId = 5, ResponsibleItem = "量檢具費", DepartemntId = "工機-量檢具" },
                new QuoteItem() { QuoteItemId = 6, ResponsibleItem = "夾治具費", DepartemntId = "工機-夾治具" },
                new QuoteItem() { QuoteItemId = 7, ResponsibleItem = "設備費", DepartemntId = "工機-設備" },
                new QuoteItem() { QuoteItemId = 8, ResponsibleItem = "總成組立費", DepartemntId = "營業" },
                new QuoteItem() { QuoteItemId = 9, ResponsibleItem = "包裝&運輸費", DepartemntId = "生管" },
                new QuoteItem() { QuoteItemId = 10, ResponsibleItem = "打樣費", DepartemntId = "採購" },
                new QuoteItem() { QuoteItemId = 11, ResponsibleItem = "試驗費", DepartemntId = "試驗課" }
            );

            // DataSeeding (Authorization)
            string superAdminName = "SuperAdmin";
            string superAdminRoleDesc = "超級管理員";
            var superAdminRole = new AdminRole() { Id = superAdminName, Name = superAdminName, NormalizedName = superAdminName.ToUpper(), RoleDesc = superAdminRoleDesc };
            superAdminRole.ConcurrencyStamp = "ConcurrencyStamp"; //為了防止再移轉
            modelBuilder.Entity<AdminRole>().HasData(superAdminRole);

            string adminName = "Admin";
            string adminRoleDesc = "系統管理員";
            var adminRole = new AdminRole() { Id = adminName, Name = adminName, NormalizedName = adminName.ToUpper(), RoleDesc = adminRoleDesc };
            adminRole.ConcurrencyStamp = "ConcurrencyStamp"; //為了防止再移轉
            modelBuilder.Entity<AdminRole>().HasData(adminRole);

            // DataSeeding (Identity)
            string userName = "sadmin";
            string email = "sadmin@hcmfgroup.com";
            string password = "sadmin";
            var user = new AdminUser() { Id = userName, DepartmentId = "營業", UserName = userName, NormalizedUserName = userName.ToUpper(), Email = email, NormalizedEmail = email.ToUpper(), SecurityStamp = Guid.NewGuid().ToString() };
            // user.PasswordHash = _passwordHasher.HashPassword(user, password);
            user.PasswordHash = "AQAAAAEAACcQAAAAEOtRfNDmY3fKqd9iqJINpOVUiLz8JFKzKEz/Xt46A/eIfMdpdMjueu4xYIYFRncnXg=="; //為了防止再移轉
            user.SecurityStamp = "SecurityStamp"; //為了防止再移轉
            user.ConcurrencyStamp = "ConcurrencyStamp"; //為了防止再移轉
            modelBuilder.Entity<AdminUser>().HasData(user);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>() { UserId = user.Id, RoleId = superAdminRole.Id }
            );
        }
    }
}