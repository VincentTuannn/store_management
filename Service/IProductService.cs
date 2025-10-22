using store_management.DTO;

namespace store_management.Service
{
    public interface IProductService
    {
        Task<List<productsDTO>> GetAllProductsAsync();
        Task<productsDTO> GetProductByIdAsync(int id);
        Task<productsDTO> CreateProductAsync(productsDTO dto);
        Task UpdateProductAsync(int id, productsDTO dto);
        Task DeleteProductAsync(int id);
    }
}
