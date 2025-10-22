using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IProductRepository _productRepository;  
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderService _orderService;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IProductRepository productRepository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ordersDTO> CreateOrderAsync(ordersDTO dto)
        {
            // Business logic: Check customer tồn tại, tính total từ order items, check inventory
            var customer = _customerRepository.GetCustomerByID(dto.Customer_id);  // Giả sử inject thêm
            if (customer == null) throw new KeyNotFoundException("Customer not found.");

            // Tính total_amount từ order_items (giả sử logic)
            dto.Total_amount = CalculateTotalFromItems(dto.OrderItems);  // Method tùy chỉnh
            dto.Discount_amount = CalculateDiscount(dto);  // Check promotion nếu có

            var order = _mapper.Map<Orders>(dto);
            _repository.InsertOrder(order);
            _repository.Save();

            // Update inventory sau tạo order
            UpdateInventoryForOrder(order);

            return _mapper.Map<ordersDTO>(order);
        }

        private decimal CalculateTotalFromItems(List<order_itemsDTO> items)
        {
            return items.Sum(i => i.Subtotal);  // Ví dụ tính tổng
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = _repository.GetOrderByID(id);
            if (order == null) throw new KeyNotFoundException();
            if (order.Status == "paid") throw new InvalidOperationException("Cannot delete paid order.");

            _repository.DeleteOrder(id);
            _repository.Save();
        }

        private void UpdateInventoryForOrder(Orders order)
        {
            foreach (var item in order.OrderItems)  // Giả sử navigation
            {
                var inventory = _inventoryRepository.GetInventoryByProductId(item.ProductId);
                inventory.Quantity -= item.Quantity;
                _inventoryRepository.UpdateInventory(inventory);
            }
            _inventoryRepository.Save();
        }

        public async Task<List<ordersDTO>> GetAllOrdersAsync()
        {
            var orders = await Task.FromResult(_repository.GetOrders());
            return _mapper.Map<List<ordersDTO>>(orders);
        }

        public async Task<ordersDTO> GetOrderByIdAsync(int id)
        {
            var order = _repository.GetOrderByID(id);
            if (order == null) throw new KeyNotFoundException($"Order with ID {id} not found.");
            return _mapper.Map<ordersDTO>(order);
        }

        public async Task UpdateOrderStatusAsync(int id, string status)
        {
            // Validate status
            if (!new[] { "pending", "paid", "canceled" }.Contains(status))
                throw new ArgumentException("Invalid status.");

            var order = _repository.GetOrderByID(id);
            if (order == null) throw new KeyNotFoundException();

            order.Status = status;
            _repository.UpdateOrder(order);
            _repository.Save();
        }
    }
}
