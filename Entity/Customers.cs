using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace store_management.Entity
{
    [Table("customers")]
    public class Customers
    {
        // Primary Key
        [Key]  // Bắt buộc: Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tự động tăng ID
        public int CustomerId { get; set; }  // Đổi tên thành PascalCase theo convention C#

        // Các properties khác (mapping từ DTO)
        [Required]  // Không null
        [MaxLength(100)]  // Giới hạn độ dài tên
        [Column("Name")]  // Tùy chọn: Tên cột DB
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]  // Validate định dạng email
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]  // Số điện thoại, tùy chỉnh theo quốc gia
        public string Phone { get; set; } = string.Empty;

        [MaxLength(255)]  // Địa chỉ có thể dài
        public string Address { get; set; } = string.Empty;  // Không required nếu optional

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default UTC

        // Thêm UpdatedAt (nullable, tự động set khi update)
        public DateTime? UpdatedAt { get; set; }

        // Timestamp cho concurrency (tùy chọn, byte[] tự generate bởi DB)
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Constructor parameterless (yêu cầu EF Core)
        public Customers()
        {
            // Init nếu cần thêm
        }
    }
}
