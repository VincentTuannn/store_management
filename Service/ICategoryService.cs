using store_management.DTO;

namespace store_management.Service
{
    public interface ICategoryService
    {
        Task<List<categoriesDTO>> GetAllCategoriesAsync();
        Task<categoriesDTO> GetCategoryByIdAsync(int id);
        Task<categoriesDTO> CreateCategoryAsync(categoriesDTO dto);
        Task UpdateCategoryAsync(int id, categoriesDTO dto);
        Task DeleteCategoryAsync(int id);
    }
}
