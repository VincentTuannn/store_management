using store_management.Entity;

namespace store_management.Repository
{
    public interface IOrderItemRepository : IDisposable
    {
        IEnumerable<OrderItems> GetOrderItems();
        OrderItems GetOrderItemByID(int order_item_id);
        void InsertOrderItem(OrderItems orderItem);
        void DeleteOrderItem(int order_item_id);
        void UpdateOrderItem(OrderItems orderItem);
        List<OrderItems> GetOrderItemsByOrderId(int orderId);
        void Save();
    }
}
