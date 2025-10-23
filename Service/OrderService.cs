using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;  
        private readonly ICustomerRepository _customerRepository;
        private readonly IPromotionService _promotionService;
        private readonly IOrderService _orderService;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository repository, IProductRepository productRepository,
            ICustomerRepository customerRepository, IPromotionService promotionService,
            IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _promotionService = promotionService ?? throw new ArgumentNullException(nameof(promotionService));
            _inventoryRepository = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ordersDTO> CreateOrderAsync(ordersDTO dto, List<order_itemsDTO> items)
        {
            // Check customer tồn tại
            var customer = _customerRepository.GetCustomerByID(dto.Customer_id);
            if (customer == null) throw new KeyNotFoundException("Customer not found.");

            // Check items không rỗng
            if (items == null || !items.Any()) throw new ArgumentException("Order must have at least one item.");

            // Tính Total_amount từ sum(Subtotal của items)
            dto.Total_amount = CalculateTotalFromItems(items);

            // Tính Discount_amount từ Promo_id
            dto.Discount_amount = await CalculateDiscount(dto.Promo_id, dto.Total_amount);

            // Tạo Order trước (không có items)
            var order = _mapper.Map<Orders>(dto);
            _repository.InsertOrder(order);
            _repository.Save();  // Commit để lấy OrderId tự tăng

            return _mapper.Map<ordersDTO>(order);  // Trả DTO (Total và Discount đã tính)
        }

        private decimal CalculateTotalFromItems(List<order_itemsDTO> items)
        {
            return items.Sum(i => i.Subtotal);  // Ví dụ tính tổng
        }

        //Tính discount từ Promotion
        private async Task<decimal> CalculateDiscount(int? promoId, decimal orderAmount)
        {
            if (!promoId.HasValue) return 0;  // Không có promo

            try
            {
                return await _promotionService.CalculateDiscountAsync(promoId.Value, orderAmount);
            }
            catch
            {
                return 0;  // Fallback nếu lỗi
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = _repository.GetOrderByID(id);
            if (order == null) throw new KeyNotFoundException();
            if (order.Status == "paid") throw new InvalidOperationException("Cannot delete paid order.");

            _repository.DeleteOrder(id);
            _repository.Save();
        }

        //private void UpdateInventoryForOrder(Orders order)
        //{
        //    var items = _orderItemRepository.GetOrderItemsByOrderId(order.OrderId);  // Giả sử method trong Repository

        //    foreach (var item in items)
        //    {
        //        var inventory = _inventoryRepository.GetInventoryByProductId(item.ProductId);  // PascalCase
        //        if (inventory != null)
        //        {
        //            inventory.Quantity -= item.Quantity;  // PascalCase
        //            _inventoryRepository.UpdateInventory(inventory);
        //        }
        //    }
        //    _inventoryRepository.Save();
        //}

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
