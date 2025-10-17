using store_management.Entity;

namespace store_management.Repository
{
    public interface IOrderItemRepository : IDisposable
    {
        IEnumerable<OrderItems> GetOrderItems();
        OrderItems GetOrderItemByID(int order_item_id);
        void UpdateOrderItem(OrderItems orderItem);
        void Save();
    }
}
