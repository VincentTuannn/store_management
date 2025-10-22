using store_management.DTO;

namespace store_management.Service
{
    public interface ICustomerService
    {
        Task<List<customersDTO>> GetAllCustomersAsync();
        Task<customersDTO> GetCustomerByIdAsync(int id);
        Task<customersDTO> CreateCustomerAsync(customersDTO dto);
        Task UpdateCustomerAsync(int id, customersDTO dto);
        Task DeleteCustomerAsync(int id);
    }
}
