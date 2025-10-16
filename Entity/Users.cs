using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace store_management.Entity
{
    [Table("users")]
    public class Users
    {
        [Key]  // Bắt buộc: Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tự động tăng ID
        public int UserId { get; set; }

        // Các properties khác (mapping từ DTO)
        [Required]  // Không null
        [MaxLength(50)]  // Giới hạn độ dài username (tùy chỉnh)
        [Column("Username")]  // Tùy chọn: Tên cột DB
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]  // Password nên hash, độ dài lớn hơn
        public string Password { get; set; } = string.Empty;  // Lưu ý: Hash trước khi save!

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]  // Role như "Admin", "User"
        public string Role { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Default UTC

        // Thêm UpdatedAt (nullable, tự động set khi update)
        public DateTime? UpdatedAt { get; set; }

        // Timestamp cho concurrency (tùy chọn, byte[] tự generate bởi DB)
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Constructor parameterless (yêu cầu EF Core)
        public Users()
        {
            // Init nếu cần thêm
        }
    }
}
