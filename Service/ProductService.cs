using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;  
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<productsDTO> CreateProductAsync(productsDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Product_name)) throw new ArgumentException("Product name is required.");
            if (!string.IsNullOrWhiteSpace(dto.Barcode) && _repository.GetProducts().Any(p => p.Barcode == dto.Barcode))
                throw new InvalidOperationException("Barcode already exists.");

            var category = _categoryRepository.GetCategoryByID(dto.Category_id);
            if (category == null) throw new KeyNotFoundException("Category not found.");

            var product = _mapper.Map<Products>(dto);
            _repository.InsertProduct(product);
            _repository.Save();
            return _mapper.Map<productsDTO>(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = _repository.GetProductByID(id);
            if (product == null) throw new KeyNotFoundException();
            if (_inventoryRepository.GetInventoryByProductId(id).Quantity > 0) 
                throw new InvalidOperationException("Cannot delete product with stock.");

            _repository.DeleteProduct(id);
            _repository.Save();
        }

        public async Task<List<productsDTO>> GetAllProductsAsync()
        {
            var products = await Task.FromResult(_repository.GetProducts());
            return _mapper.Map<List<productsDTO>>(products);
        }

        public async Task<productsDTO> GetProductByIdAsync(int id)
        {
            var product = _repository.GetProductByID(id);
            if (product == null) throw new KeyNotFoundException($"Product with ID {id} not found.");
            return _mapper.Map<productsDTO>(product);
        }

        public async Task UpdateProductAsync(int id, productsDTO dto)
        {
            var product = _repository.GetProductByID(id);
            if (product == null) throw new KeyNotFoundException();

            if (dto.Category_id > 0)
            {
                var category = _categoryRepository.GetCategoryByID(dto.Category_id);
                if (category == null) throw new KeyNotFoundException("Category not found.");
            }

            _mapper.Map(dto, product);
            _repository.UpdateProduct(product);
            _repository.Save();
        }
    }
}
