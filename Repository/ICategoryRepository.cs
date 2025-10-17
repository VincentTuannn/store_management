using store_management.Entity;

namespace store_management.Repository
{
    public interface ICategoryRepository : IDisposable
    {
        IEnumerable<Categories> GetCategories();
        Categories GetCategoryByID(int category_id);
        void InsertCategory(Categories category);
        void DeleteCategory(int category_id);
        void UpdateCategory(Categories category);
        void Save();
    }
}
