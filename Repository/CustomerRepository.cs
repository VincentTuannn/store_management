using Microsoft.EntityFrameworkCore;
using store_management.Data;
using store_management.Entity;

namespace store_management.Repository
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private AppDbContext context;
        public CustomerRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteCustomer(int customer_id)
        {
            Customers customer = context.Customers.Find(customer_id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Customers GetCustomerByID(int customer_id)
        {
            return context.Customers.Find(customer_id);
        }

        public IEnumerable<Customers> GetCustomers()
        {
            return context.Customers.ToList();
        }

        public void InsertCustomer(Customers customer)
        {
            context.Customers.Add(customer);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateCustomer(Customers customer)
        {
            context.Entry(customer).State = EntityState.Modified;
        }
    }
}
