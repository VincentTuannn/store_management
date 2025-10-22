using AutoMapper;
using store_management.DTO;
using store_management.Repository;

namespace store_management.Service
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _repository;
        private readonly IMapper _mapper;

        public InventoryService(IInventoryRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task DeleteInventoryAsync(int id)
        {
            var inventory = _repository.GetInventoryByID(id);
            if (inventory == null) throw new KeyNotFoundException($"Inventory with ID {id} not found.");

            _repository.DeleteInventory(id);
            _repository.Save();
        }

        public async Task<List<inventoryDTO>> GetAllInventoriesAsync()
        {
            var inventories = await Task.FromResult(_repository.GetInventories());
            return _mapper.Map<List<inventoryDTO>>(inventories);
        }

        public async Task<inventoryDTO> GetInventoryByIdAsync(int id)
        {
            var inventory = _repository.GetInventoryByID(id);
            if (inventory == null) throw new KeyNotFoundException($"Inventory with ID {id} not found.");
            return _mapper.Map<inventoryDTO>(inventory);
        }

        public async Task<inventoryDTO> UpdateInventoryQuantityAsync(int productId, int quantity)
        {
            if (quantity < 0) throw new ArgumentException("Quantity cannot be negative.");

            var inventory = _repository.GetInventoryByProductId(productId);  
            if (inventory == null) throw new KeyNotFoundException($"Inventory for product {productId} not found.");

            inventory.Quantity = quantity;  // Update logic
            _repository.UpdateInventory(inventory);
            _repository.Save();
            return _mapper.Map<inventoryDTO>(inventory);
        }
    }
}
