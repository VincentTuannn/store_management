using store_management.DTO;

namespace store_management.Service
{
    public interface IOrderService
    {
        Task<List<ordersDTO>> GetAllOrdersAsync();
        Task<ordersDTO> GetOrderByIdAsync(int id);
        Task<ordersDTO> CreateOrderAsync(ordersDTO dto, List<order_itemsDTO> items);  // Tính total, check inventory
        Task UpdateOrderStatusAsync(int id, string status);
        Task DeleteOrderAsync(int id);
    }
}
