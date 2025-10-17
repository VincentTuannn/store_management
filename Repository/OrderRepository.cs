using Microsoft.EntityFrameworkCore;
using store_management.Data;
using store_management.Entity;

namespace store_management.Repository
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private AppDbContext context;
        public OrderRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteOrder(int order_id)
        {
            Orders order = context.Orders.Find(order_id);
            if (order != null)
            {
                context.Orders.Remove(order);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Orders GetOrderByID(int order_id)
        {
            return context.Orders.Find(order_id);
        }

        public IEnumerable<Orders> GetOrders()
        {
            return context.Orders.ToList();
        }

        public void InsertOrder(Orders order)
        {
            context.Orders.Add(order);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateOrder(Orders order)
        {
            context.Entry(order).State = EntityState.Modified;
        }
    }
}
