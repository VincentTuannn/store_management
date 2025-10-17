using store_management.Entity;

namespace store_management.Repository
{
    public interface IPaymentRepository : IDisposable
    {
        IEnumerable<Payments> GetPayments();
        Payments GetPaymentByID(int payment_id);
        void InsertPayment(Payments payment);
        void DeletePayment(int payment_id);
        void UpdatePayment(Payments payment);
        void Save();
    }
}
