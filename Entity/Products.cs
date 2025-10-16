using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace store_management.Entity
{
    [Table("products")]
    public class Products
    {
        // Primary Key
        [Key]  // Bắt buộc: Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tự động tăng ID
        public int ProductId { get; set; }  // Đổi tên thành PascalCase theo convention C#

        // Foreign Key cho Category
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]  // Tùy chọn: Chỉ định FK rõ ràng
        public virtual Categories Category { get; set; } = null!;  // Navigation property

        // Foreign Key cho Supplier
        [Required]
        public int SupplierId { get; set; }
        [ForeignKey(nameof(SupplierId))]
        public virtual Suppliers Supplier { get; set; } = null!;  // Navigation property

        // Các properties khác (mapping từ DTO)
        [Required]  // Không null
        [MaxLength(200)]  // Giới hạn độ dài tên sản phẩm
        [Column("ProductName")]  // Tùy chọn: Tên cột DB
        public string ProductName { get; set; } = string.Empty;

        [MaxLength(50)]  // Mã vạch thường ngắn
        public string Barcode { get; set; } = string.Empty;  // Optional nếu không bắt buộc

        [Required]
        [Column(TypeName = "decimal(18,2)")]  // Chính xác 2 chữ số thập phân cho giá
        public decimal Price { get; set; }

        [Required]
        [MaxLength(20)]  // Đơn vị như "kg", "cái"
        public string Unit { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default UTC

        // Thêm UpdatedAt (nullable, tự động set khi update)
        public DateTime? UpdatedAt { get; set; }

        // Timestamp cho concurrency (tùy chọn, byte[] tự generate bởi DB)
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Constructor parameterless (yêu cầu EF Core)
        public Products()
        {
            // Init nếu cần thêm
        }
    }
}
