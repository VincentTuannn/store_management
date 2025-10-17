using Microsoft.EntityFrameworkCore;
using store_management.Data;
using store_management.Entity;

namespace store_management.Repository
{
    public class SupplierRepository : ISupplierRepository, IDisposable
    {
        private AppDbContext context;
        public SupplierRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteSupplier(int supplier_id)
        {
            Suppliers supplier = context.Suppliers.Find(supplier_id);
            if (supplier != null)
            {
                context.Suppliers.Remove(supplier);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Suppliers GetSupplierByID(int supplier_id)
        {
            return context.Suppliers.Find(supplier_id);
        }

        public IEnumerable<Suppliers> GetSuppliers()
        {
            return context.Suppliers.ToList();
        }

        public void InsertSupplier(Suppliers supplier)
        {
            context.Suppliers.Add(supplier);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateSupplier(Suppliers supplier)
        {
            context.Entry(supplier).State = EntityState.Modified;
        }
    }
}
