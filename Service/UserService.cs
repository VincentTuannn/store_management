using AutoMapper;
using store_management.DTO;
using store_management.Entity;
using store_management.Repository;

namespace store_management.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;  // Inject Repository
        private readonly IMapper _mapper;  // Inject AutoMapper

        // Constructor: Inject dependencies qua DI
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Tạo user mới
        public async Task<usersDTO> CreateUserAsync(usersDTO dto)
        {
            // Business logic: Validate (ví dụ: check username unique)
            if (string.IsNullOrWhiteSpace(dto.Username))
            {
                throw new ArgumentException("Username is required.");
            }
            if (_repository.GetUsers().Any(u => u.Username == dto.Username))
            {
                throw new InvalidOperationException("Username already exists.");
            }

            // Hash password (thực tế dùng BCrypt hoặc ASP.NET Identity)
            // dto.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);  // Uncomment nếu cần

            var user = _mapper.Map<Users>(dto);  // Map DTO → Entity
            _repository.InsertUser(user);  // Gọi Repository
            _repository.Save();  // Commit

            return _mapper.Map<usersDTO>(user);  // Trả DTO sau tạo
        }

        // Xóa user
        public async Task DeleteUserAsync(int id)
        {
            var user = _repository.GetUserByID(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            // Business logic: Không cho xóa admin
            if (user.Role == "admin")
            {
                throw new UnauthorizedAccessException("Cannot delete admin user.");
            }

            _repository.DeleteUser(id);  // Gọi Repository
            _repository.Save();  // Commit
        }

        // Lấy tất cả users
        public async Task<List<usersDTO>> GetAllUsersAsync()
        {
            var users = await Task.FromResult(_repository.GetUsers());  // Gọi Repository (sync → async wrapper)
            return _mapper.Map<List<usersDTO>>(users);  // Map Entity → DTO
        }

        public async Task<usersDTO> GetUserByIdAsync(int id)
        {
            var user = _repository.GetUserByID(id);  // Gọi Repository → Entity
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");  // Business exception
            }
            return _mapper.Map<usersDTO>(user);  // Map → DTO
        }

        // Cập nhật user
        public async Task UpdateUserAsync(int id, usersDTO dto)
        {
            var user = _repository.GetUserByID(id);  // Lấy Entity
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }

            // Business logic: Chỉ update nếu role là staff (ví dụ: admin không cho update role)
            if (dto.Role != user.Role && user.Role == "admin")
            {
                throw new UnauthorizedAccessException("Cannot update admin role.");
            }

            _mapper.Map(dto, user);  // Map DTO → Entity (update fields)
            _repository.UpdateUser(user);  // Gọi Repository
            _repository.Save();  // Commit
        }
    }
}
