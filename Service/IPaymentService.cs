using store_management.DTO;

namespace store_management.Service
{
    public interface IPaymentService
    {
        Task<List<paymentsDTO>> GetAllPaymentsAsync();
        Task<paymentsDTO> GetPaymentByIdAsync(int id);
        Task<paymentsDTO> CreatePaymentAsync(paymentsDTO dto);
        Task UpdatePaymentAsync(int id, paymentsDTO dto);
        Task DeletePaymentAsync(int id);
    }
}
