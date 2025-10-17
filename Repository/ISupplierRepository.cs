using store_management.Entity;

namespace store_management.Repository
{
    public interface ISupplierRepository : IDisposable
    {
        IEnumerable<Suppliers> GetSuppliers();
        Suppliers GetSupplierByID(int supplier_id);
        void InsertSupplier(Suppliers supplier);
        void DeleteSupplier(int supplier_id);
        void UpdateSupplier(Suppliers supplier);
        void Save();
    }
}
