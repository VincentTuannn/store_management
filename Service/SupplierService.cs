using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public SupplierService(ISupplierRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<suppliersDTO> CreateSupplierAsync(suppliersDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Supplier name is required.");
            if (!string.IsNullOrWhiteSpace(dto.Email) && _repository.GetSuppliers().Any(s => s.Email == dto.Email))
                throw new InvalidOperationException("Email already exists.");

            var supplier = _mapper.Map<Suppliers>(dto);
            _repository.InsertSupplier(supplier);
            _repository.Save();
            return _mapper.Map<suppliersDTO>(supplier);
        }

        public async Task DeleteSupplierAsync(int id)
        {
            var supplier = _repository.GetSupplierByID(id);
            if (supplier == null) throw new KeyNotFoundException();
            if (_productRepository.GetProducts().Any(p => p.SupplierId == id))  // Check FK
                throw new InvalidOperationException("Cannot delete supplier with associated products.");

            _repository.DeleteSupplier(id);
            _repository.Save();
        }

        public async Task<List<suppliersDTO>> GetAllSuppliersAsync()
        {
            var suppliers = await Task.FromResult(_repository.GetSuppliers());
            return _mapper.Map<List<suppliersDTO>>(suppliers);
        }

        public async Task<suppliersDTO> GetSupplierByIdAsync(int id)
        {
            var supplier = _repository.GetSupplierByID(id);
            if (supplier == null) throw new KeyNotFoundException($"Supplier with ID {id} not found.");
            return _mapper.Map<suppliersDTO>(supplier);
        }

        public async Task UpdateSupplierAsync(int id, suppliersDTO dto)
        {
            var supplier = _repository.GetSupplierByID(id);
            if (supplier == null) throw new KeyNotFoundException();

            _mapper.Map(dto, supplier);
            _repository.UpdateSupplier(supplier);
            _repository.Save();
        }
    }
}
