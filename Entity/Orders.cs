using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace store_management.Entity
{
    [Table("orders")]
    public class Orders
    {
        // Primary Key
        [Key]  // Bắt buộc: Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tự động tăng ID
        public int OrderId { get; set; }  // Đổi tên thành PascalCase theo convention C#

        // Foreign Key cho Customer
        [Required]
        public int CustomerId { get; set; }
        [ForeignKey(nameof(CustomerId))]  // Tùy chọn: Chỉ định FK rõ ràng
        public virtual Customers Customer { get; set; } = null!;  // Navigation property

        // Foreign Key cho User
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual Users User { get; set; } = null!;  // Navigation property

        // Foreign Key cho Promotion (nullable)
        public int? PromoId { get; set; }
        [ForeignKey(nameof(PromoId))]
        public virtual Promotions? Promotion { get; set; }  // Nullable navigation

        [Required]
        public DateTime OrderDate { get; set; }  // Ngày đặt hàng

        [Required]
        [MaxLength(50)]  // Trạng thái như "Pending", "Shipped"
        public string Status { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]  // Chính xác 2 chữ số thập phân cho tổng tiền
        public decimal TotalAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]  // Số tiền giảm giá
        public decimal DiscountAmount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default UTC

        // Thêm UpdatedAt (nullable, tự động set khi update)
        public DateTime? UpdatedAt { get; set; }

        // Timestamp cho concurrency (tùy chọn, byte[] tự generate bởi DB)
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Constructor parameterless (yêu cầu EF Core)
        public Orders()
        {
            // Init nếu cần thêm
        }
    }
}
