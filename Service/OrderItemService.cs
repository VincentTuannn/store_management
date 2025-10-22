using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _repository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<order_itemsDTO> CreateOrderItemAsync(order_itemsDTO dto)
        {
            // Business logic: Check order và product tồn tại, tính subtotal
            if (dto.Quantity <= 0) throw new ArgumentException("Quantity must be positive.");
            var order = _orderRepository.GetOrderByID(dto.Order_id);  // Giả sử inject
            if (order == null) throw new KeyNotFoundException("Order not found.");

            var product = _productRepository.GetProductByID(dto.Product_id);
            if (product == null) throw new KeyNotFoundException("Product not found.");
            dto.Price = product.Price;  // Lấy giá hiện tại
            dto.Subtotal = dto.Price * dto.Quantity;

            var item = _mapper.Map<OrderItems>(dto);
            _repository.InsertOrderItem(item);
            _repository.Save();
            return _mapper.Map<order_itemsDTO>(item);
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var item = _repository.GetOrderItemByID(id);
            if (item == null) throw new KeyNotFoundException();

            _repository.DeleteOrderItem(id);
            _repository.Save();
        }

        public async Task<List<order_itemsDTO>> GetAllOrderItemsAsync()
        {
            var items = await Task.FromResult(_repository.GetOrderItems());
            return _mapper.Map<List<order_itemsDTO>>(items);
        }

        public async Task<order_itemsDTO> GetOrderItemByIdAsync(int id)
        {
            var item = _repository.GetOrderItemByID(id);
            if (item == null) throw new KeyNotFoundException($"OrderItem with ID {id} not found.");
            return _mapper.Map<order_itemsDTO>(item);
        }

        public async Task UpdateOrderItemAsync(int id, order_itemsDTO dto)
        {
            var item = _repository.GetOrderItemByID(id);
            if (item == null) throw new KeyNotFoundException();

            _mapper.Map(dto, item);
            item.Subtotal = item.Price * item.Quantity;  // Tính lại
            _repository.UpdateOrderItem(item);
            _repository.Save();
        }
    }
}
