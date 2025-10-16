using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace store_management.Entity
{
    [Table("categories")]
    public class Categories
    {
        // Primary Key
        [Key]  // Bắt buộc: Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tự động tăng ID
        public int CategoryId { get; set; }  // Đổi tên thành PascalCase theo convention C#

        // Các properties khác (mapping từ DTO)
        [Required]  // Không null
        [MaxLength(100)]  // Giới hạn độ dài tên loại sản phẩm
        [Column("CategoryName")]  // Tùy chọn: Tên cột DB
        public string CategoryName { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default UTC

        // Thêm UpdatedAt (nullable, tự động set khi update)
        public DateTime? UpdatedAt { get; set; }

        // Timestamp cho concurrency (tùy chọn, byte[] tự generate bởi DB)
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Constructor parameterless (yêu cầu EF Core)
        public Categories()
        {
            // Init nếu cần thêm
        }
    }
}
