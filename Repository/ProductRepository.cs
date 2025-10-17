using Microsoft.EntityFrameworkCore;
using store_management.Data;
using store_management.Entity;

namespace store_management.Repository
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private AppDbContext context;
        public ProductRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteProduct(int product_id)
        {
            Products product = context.Products.Find(product_id);
            if (product != null)
            {
                context.Products.Remove(product);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Products GetProductByID(int product_id)
        {
            return context.Products.Find(product_id);
        }

        public IEnumerable<Products> GetProducts()
        {
            return context.Products.ToList();
        }

        public void InsertProduct(Products product)
        {
            context.Products.Add(product);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateProduct(Products product)
        {
            context.Entry(product).State = EntityState.Modified;
        }
    }
}
