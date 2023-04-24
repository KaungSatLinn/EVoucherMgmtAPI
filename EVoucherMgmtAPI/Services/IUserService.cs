using EVoucherMgmtAPI.Dtos;
using EVoucherMgmtAPI.Models;

namespace EVoucherMgmtAPI.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(int id);
        Task<string> AddUser(UserDto newUser);
        Task<string> UpdateUser(UserDto updateUser);
        string CheckUser(LoginUserDto user);
        Task<UserDto> GetUserByPhone(LoginUserDto loginUser);
        void AddRefreshToken(RefreshToken refreshToken);
        RefreshToken GetRefreshToken(string refreshToken);
    }
}
