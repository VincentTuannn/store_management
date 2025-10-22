using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<paymentsDTO> CreatePaymentAsync(paymentsDTO dto)
        {
            var order = _orderRepository.GetOrderByID(dto.Order_id);  // Giả sử inject
            if (order == null) throw new KeyNotFoundException("Order not found.");
            if (dto.Amount != order.TotalAmount) throw new ArgumentException("Amount must match order total.");

            var payment = _mapper.Map<Payments>(dto);
            _repository.InsertPayment(payment);
            _repository.Save();

            // Business logic: Update order status to 'paid'
            order.Status = "paid";
            _orderRepository.UpdateOrder(order);
            _orderRepository.Save();

            return _mapper.Map<paymentsDTO>(payment);
        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = _repository.GetPaymentByID(id);
            if (payment == null) throw new KeyNotFoundException();

            _repository.DeletePayment(id);
            _repository.Save();
        }

        public async Task<List<paymentsDTO>> GetAllPaymentsAsync()
        {
            var payments = await Task.FromResult(_repository.GetPayments());
            return _mapper.Map<List<paymentsDTO>>(payments);
        }

        public async Task<paymentsDTO> GetPaymentByIdAsync(int id)
        {
            var payment = _repository.GetPaymentByID(id);
            if (payment == null) throw new KeyNotFoundException($"Payment with ID {id} not found.");
            return _mapper.Map<paymentsDTO>(payment);
        }

        public async Task UpdatePaymentAsync(int id, paymentsDTO dto)
        {
            var payment = _repository.GetPaymentByID(id);
            if (payment == null) throw new KeyNotFoundException();

            _mapper.Map(dto, payment);
            _repository.UpdatePayment(payment);
            _repository.Save();
        }
    }
}
