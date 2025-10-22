using store_management.DTO;

namespace store_management.Service
{
    public interface ISupplierService
    {
        Task<List<suppliersDTO>> GetAllSuppliersAsync();
        Task<suppliersDTO> GetSupplierByIdAsync(int id);
        Task<suppliersDTO> CreateSupplierAsync(suppliersDTO dto);
        Task UpdateSupplierAsync(int id, suppliersDTO dto);
        Task DeleteSupplierAsync(int id);
    }
}
