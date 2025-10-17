using store_management.Entity;

namespace store_management.Repository
{
    public interface ICustomerRepository : IDisposable
    {
        IEnumerable<Customers> GetCustomers();
        Customers GetCustomerByID(int customer_id);
        void InsertCustomer(Customers customer);
        void DeleteCustomer(int customer_id);
        void UpdateCustomer(Customers customer);
        void Save();
    }
}
