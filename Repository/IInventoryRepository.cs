using store_management.Entity;

namespace store_management.Repository
{
    public interface IInventoryRepository : IDisposable
    {
        IEnumerable<Inventory> GetInventories();
        Inventory GetInventoryByID(int inventory_id);
        void InsertInventory(Inventory inventory);
        void DeleteInventory(int inventory_id);
        void UpdateInventory(Inventory inventory);
        void Save();
    }
}
