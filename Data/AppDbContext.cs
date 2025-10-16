using Microsoft.EntityFrameworkCore;
using store_management.Entity;

namespace store_management.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IHostEnvironment _environment;  // Inject để kiểm tra môi trường (tùy chọn)

        // Constructor: Nhận options từ DI, và optional IHostEnvironment cho seed
        public AppDbContext(DbContextOptions<AppDbContext> options, IHostEnvironment environment = null) : base(options)
        {
            // Logic init cơ bản: Kiểm tra options không null
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "DbContext options cannot be null.");
            }

            // Logic init tùy chọn: Seed data mẫu nếu cần (chỉ ở Development)
            _environment = environment;
            if (_environment?.IsDevelopment() == true)
            {
                SeedData();  // Gọi method seed (xem dưới)
            }
        }

        // DbSet: Mỗi DbSet map với một bảng DB
        public DbSet<Users> Users { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Promotions> Promotions { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Payments> Payments { get; set; }

        // Method seed data mẫu (tùy chọn, chỉ gọi ở constructor dev)
        private void SeedData()
        {
            if (!Users.Any())  // Kiểm tra nếu bảng Users rỗng
            {
                var sampleUsers = new List<Users>
                {
                    new Users { Username = "admin", Password = "hashed_password", FullName = "Quản trị viên", Role = "admin" },
                    new Users { Username = "staff01", Password = "hashed_password", FullName = "Nguyễn Văn A", Role = "staff" }
                };
                Users.AddRange(sampleUsers);
                SaveChanges();  // Commit seed data
            }
            // Có thể thêm seed cho các bảng khác tương tự (Customers, Categories, etc.)
        }

        // Cấu hình model (relationships, constraints)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ví dụ config FK cho Products (giữ nguyên như trước)
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Unique cho username
            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
