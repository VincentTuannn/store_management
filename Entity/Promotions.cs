using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace store_management.Entity
{
    [Table("promotions")]
    public class Promotions
    {
        // Primary Key
        [Key]  // Bắt buộc: Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tự động tăng ID
        public int PromotionId { get; set; }  // Đổi tên thành PascalCase theo convention C#

        // Các properties khác (mapping từ DTO)
        [Required]  // Không null
        [MaxLength(50)]  // Mã khuyến mãi thường ngắn
        [Column("PromotionCode")]  // Tùy chọn: Tên cột DB
        public string PromotionCode { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]  // Mô tả có thể dài
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]  // Loại như "Percentage", "Fixed"
        public string DiscountType { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]  // Chính xác 2 chữ số thập phân cho giá trị
        public decimal DiscountValue { get; set; }

        [Required]
        public DateTime StartDate { get; set; }  // Ngày bắt đầu

        [Required]
        public DateTime EndDate { get; set; }  // Ngày kết thúc

        [Required]
        [Column(TypeName = "decimal(18,2)")]  // Số tiền tối thiểu
        public decimal MinOrderAmount { get; set; }

        [Required]
        [Range(0, int.MaxValue)]  // Giới hạn sử dụng >=0
        public int UsageLimit { get; set; }

        [Required]
        [Range(0, int.MaxValue)]  // Số lần đã dùng >=0
        public int UsedCount { get; set; }

        [Required]
        [MaxLength(20)]  // Trạng thái như "Active", "Expired"
        public string Status { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default UTC

        // Thêm UpdatedAt (nullable, tự động set khi update)
        public DateTime? UpdatedAt { get; set; }

        // Timestamp cho concurrency (tùy chọn, byte[] tự generate bởi DB)
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Constructor parameterless (yêu cầu EF Core)
        public Promotions()
        {
            // Init nếu cần thêm
        }
    }
}
