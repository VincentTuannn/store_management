using Microsoft.EntityFrameworkCore;
using store_management.Data;
using store_management.Entity;

namespace store_management.Repository
{
    public class InventoryRepository : IInventoryRepository, IDisposable
    {
        private AppDbContext context;
        public InventoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteInventory(int inventory_id)
        {
            Inventory inventory = context.Inventory.Find(inventory_id);
            if (inventory != null)
            {
                context.Inventory.Remove(inventory);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Inventory> GetInventories()
        {
            return context.Inventory.ToList();
        }

        public Inventory GetInventoryByID(int inventory_id)
        {
            return context.Inventory.Find(inventory_id);
        }

        public Inventory GetInventoryByProductId(int productId)
        {
            return context.Inventory.FirstOrDefault(i => i.ProductId == productId);
        }

        public void InsertInventory(Inventory inventory)
        {
            context.Inventory.Add(inventory);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateInventory(Inventory inventory)
        {
            context.Entry(inventory).State = EntityState.Modified;
        }
    }
}
