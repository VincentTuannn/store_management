using Microsoft.EntityFrameworkCore;
using store_management.Data;
using store_management.Entity;

namespace store_management.Repository
{
    public class CategoryRepository : ICategoryRepository, IDisposable
    {
        private AppDbContext context;
        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public void DeleteCategory(int category_id)
        {
            Categories category = context.Categories.Find(category_id);
            if (category != null)
            {
                context.Categories.Remove(category);
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Categories> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Categories GetCategoryByID(int category_id)
        {
            return context.Categories.Find(category_id);
        }

        public void InsertCategory(Categories category)
        {
            context.Categories.Add(category);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateCategory(Categories category)
        {
            context.Entry(category).State = EntityState.Modified;
        }
    }
}
