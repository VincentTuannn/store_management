using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace store_management.Entity
{
    [Table("inventory")]
    public class Inventory
    {
        // Primary Key
        [Key]  // Bắt buộc: Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Tự động tăng ID
        public int InventoryId { get; set; }  // Đổi tên thành PascalCase theo convention C#

        // Foreign Key cho Product
        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]  // Tùy chọn: Chỉ định FK rõ ràng
        public virtual Products Product { get; set; } = null!;  // Navigation property

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be non-negative")]  // Số lượng >=0
        public int Quantity { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;  // Default UTC nếu không set

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;  // Thêm để theo pattern

        // Timestamp cho concurrency (tùy chọn, byte[] tự generate bởi DB)
        [Timestamp]
        public byte[] RowVersion { get; set; } = null!;

        // Constructor parameterless (yêu cầu EF Core)
        public Inventory()
        {
            // Init nếu cần thêm
        }
    }
}
