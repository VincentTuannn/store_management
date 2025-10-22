using store_management.DTO;

namespace store_management.Service
{
    public interface IInventoryService
    {
        Task<List<inventoryDTO>> GetAllInventoriesAsync();
        Task<inventoryDTO> GetInventoryByIdAsync(int id);
        Task<inventoryDTO> UpdateInventoryQuantityAsync(int productId, int quantity);  // Đặc biệt: Update quantity
        Task DeleteInventoryAsync(int id);
    }
}
