using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<customersDTO> CreateCustomerAsync(customersDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Customer name is required.");
            if (!string.IsNullOrWhiteSpace(dto.Email) && _repository.GetCustomers().Any(c => c.Email == dto.Email))
                throw new InvalidOperationException("Email already exists.");

            var customer = _mapper.Map<Customers>(dto);
            _repository.InsertCustomer(customer);
            _repository.Save();
            return _mapper.Map<customersDTO>(customer);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = _repository.GetCustomerByID(id);
            if (customer == null) throw new KeyNotFoundException($"Customer with ID {id} not found.");
            if (_orderRepository.GetOrders().Any(o => o.CustomerId == id))  
                throw new InvalidOperationException("Cannot delete customer with associated orders.");

            _repository.DeleteCustomer(id);
            _repository.Save();
        }

        public async Task<List<customersDTO>> GetAllCustomersAsync()
        {
            var customers = await Task.FromResult(_repository.GetCustomers());
            return _mapper.Map<List<customersDTO>>(customers);
        }

        public async Task<customersDTO> GetCustomerByIdAsync(int id)
        {
            var customer = _repository.GetCustomerByID(id);
            if (customer == null) throw new KeyNotFoundException($"Customer with ID {id} not found.");
            return _mapper.Map<customersDTO>(customer);
        }

        public async Task UpdateCustomerAsync(int id, customersDTO dto)
        {
            var customer = _repository.GetCustomerByID(id);
            if (customer == null) throw new KeyNotFoundException($"Customer with ID {id} not found.");

            _mapper.Map(dto, customer);
            _repository.UpdateCustomer(customer);
            _repository.Save();
        }
    }
}
