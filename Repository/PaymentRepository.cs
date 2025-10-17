using Microsoft.EntityFrameworkCore;
using store_management.Data;
using store_management.Entity;

namespace store_management.Repository
{
    public class PaymentRepository : IPaymentRepository, IDisposable
    {
        private AppDbContext context;
        public PaymentRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeletePayment(int payment_id)
        {
            Payments payment = context.Payments.Find(payment_id);
            if (payment != null)
            {
                context.Payments.Remove(payment);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Payments GetPaymentByID(int payment_id)
        {
            return context.Payments.Find(payment_id);
        }

        public IEnumerable<Payments> GetPayments()
        {
            return context.Payments.ToList();
        }

        public void InsertPayment(Payments payment)
        {
            context.Payments.Add(payment);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdatePayment(Payments payment)
        {
            context.Entry(payment).State = EntityState.Modified;
        }
    }
}
