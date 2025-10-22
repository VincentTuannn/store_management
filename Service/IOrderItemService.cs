using store_management.DTO;

namespace store_management.Service
{
    public interface IOrderItemService
    {
        Task<List<order_itemsDTO>> GetAllOrderItemsAsync();
        Task<order_itemsDTO> GetOrderItemByIdAsync(int id);
        Task<order_itemsDTO> CreateOrderItemAsync(order_itemsDTO dto);
        Task UpdateOrderItemAsync(int id, order_itemsDTO dto);
        Task DeleteOrderItemAsync(int id);
    }
}
