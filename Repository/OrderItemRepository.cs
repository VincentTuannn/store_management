using Microsoft.EntityFrameworkCore;
using store_management.Data;
using store_management.Entity;

namespace store_management.Repository
{
    public class OrderItemRepository : IOrderItemRepository, IDisposable
    {
        private AppDbContext context;
        public OrderItemRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public OrderItems GetOrderItemByID(int order_item_id)
        {
            return context.OrderItems.Find(order_item_id);
        }

        public IEnumerable<OrderItems> GetOrderItems()
        {
            return context.OrderItems.ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateOrderItem(OrderItems orderItem)
        {
            context.Entry(orderItem).State = EntityState.Modified;
        }
    }
}
