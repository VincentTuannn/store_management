using store_management.DTO;

namespace store_management.Service
{
    public interface IUserService
    {
        // Lấy tất cả users (trả DTO)
        Task<List<usersDTO>> GetAllUsersAsync();

        // Lấy user theo ID (trả DTO, throw nếu không tìm thấy)
        Task<usersDTO> GetUserByIdAsync(int id);

        // Tạo user mới (nhận DTO, validate, trả DTO sau tạo)
        Task<usersDTO> CreateUserAsync(usersDTO dto);

        // Cập nhật user (nhận ID và DTO, validate, không trả gì)
        Task UpdateUserAsync(int id, usersDTO dto);

        // Xóa user (nhận ID, check quyền, không trả gì)
        Task DeleteUserAsync(int id);
    }
}
