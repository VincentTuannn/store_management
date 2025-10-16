using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace store_management.Entity
{
    [Table("order_items")]
    public class OrderItems
    {
        // Primary Key
        [Key]  // Bắt buộc: Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tự động tăng ID
        public int OrderItemId { get; set; }  // Đổi tên thành PascalCase theo convention C#

        // Foreign Key cho Order
        [Required]
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]  // Tùy chọn: Chỉ định FK rõ ràng
        public virtual Orders Order { get; set; } = null!;  // Navigation property

        // Foreign Key cho Product
        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public virtual Products Product { get; set; } = null!;  // Navigation property

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]  // Số lượng >=1
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]  // Chính xác 2 chữ số thập phân cho giá
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]  // Tổng tiền cho item
        public decimal Subtotal { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default UTC

        // Thêm UpdatedAt (nullable, tự động set khi update)
        public DateTime? UpdatedAt { get; set; }

        // Timestamp cho concurrency (tùy chọn, byte[] tự generate bởi DB)
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Constructor parameterless (yêu cầu EF Core)
        public OrderItems()
        {
            // Init nếu cần thêm
        }
    }
}
