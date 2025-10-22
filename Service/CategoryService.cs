using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<categoriesDTO> CreateCategoryAsync(categoriesDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Category_name)) throw new ArgumentException("Category name is required.");
            if (_repository.GetCategories().Any(c => c.CategoryName == dto.Category_name))
                throw new InvalidOperationException("Category name already exists.");

            var category = _mapper.Map<Categories>(dto);
            _repository.InsertCategory(category);
            _repository.Save();
            return _mapper.Map<categoriesDTO>(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = _repository.GetCategoryByID(id);
            if (category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");
            if (_productRepository.GetProducts().Any(p => p.CategoryId == id))  
                throw new InvalidOperationException("Cannot delete category with associated products.");

            _repository.DeleteCategory(id);
            _repository.Save();
        }

        public async Task<List<categoriesDTO>> GetAllCategoriesAsync()
        {
            var categories = await Task.FromResult(_repository.GetCategories());
            return _mapper.Map<List<categoriesDTO>>(categories);
        }

        public async Task<categoriesDTO> GetCategoryByIdAsync(int id)
        {
            var category = _repository.GetCategoryByID(id);
            if (category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");
            return _mapper.Map<categoriesDTO>(category);
        }

        public async Task UpdateCategoryAsync(int id, categoriesDTO dto)
        {
            var category = _repository.GetCategoryByID(id);
            if (category == null) throw new KeyNotFoundException($"Category with ID {id} not found.");

            _mapper.Map(dto, category);
            _repository.UpdateCategory(category);
            _repository.Save();
        }
    }
}
