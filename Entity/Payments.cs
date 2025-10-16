using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace store_management.Entity
{
    [Table("payments")]
    public class Payments
    {
        // Primary Key
        [Key]  // Bắt buộc: Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tự động tăng ID
        public int PaymentId { get; set; }  // Đổi tên thành PascalCase theo convention C#

        // Foreign Key cho Order
        [Required]
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]  // Tùy chọn: Chỉ định FK rõ ràng
        public virtual Orders Order { get; set; } = null!;  // Navigation property

        [Required]
        [Column(TypeName = "decimal(18,2)")]  // Chính xác 2 chữ số thập phân cho số tiền
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }  // Ngày thanh toán

        [Required]
        [MaxLength(50)]  // Phương thức như "Cash", "Card", "Bank Transfer"
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default UTC

        // Thêm UpdatedAt (nullable, tự động set khi update)
        public DateTime? UpdatedAt { get; set; }

        // Timestamp cho concurrency (tùy chọn, byte[] tự generate bởi DB)
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Constructor parameterless (yêu cầu EF Core)
        public Payments()
        {
            // Init nếu cần thêm
        }
    }
}
