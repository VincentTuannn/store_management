using store_management.Entity;

namespace store_management.Repository
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Products> GetProducts();
        Products GetProductByID(int product_id);
        void InsertProduct(Products product);
        void DeleteProduct(int product_id);
        void UpdateProduct(Products product);
        void Save();
    }
}
