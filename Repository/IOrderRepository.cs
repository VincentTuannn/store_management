using store_management.Entity;

namespace store_management.Repository
{
    public interface IOrderRepository : IDisposable
    {
        IEnumerable<Orders> GetOrders();
        Orders GetOrderByID(int order_id);
        void InsertOrder(Orders order);
        void DeleteOrder(int order_id);
        void UpdateOrder(Orders order);
        void Save();
    }
}
