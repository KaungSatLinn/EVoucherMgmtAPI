using AutoMapper;
using EVoucherMgmtAPI.Data;
using EVoucherMgmtAPI.Dtos;
using EVoucherMgmtAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoucherMgmtAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public UserService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var dbUsers = await _dataContext.User.Where(x => x.IsDelete == 0).ToListAsync();
            var users = dbUsers.Select(_mapper.Map<UserDto>).ToList();
            return users;
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var dbUser = await _dataContext.User.FirstOrDefaultAsync(x => x.Id == id && x.IsDelete == 0);
            var user = _mapper.Map<UserDto>(dbUser);
            return user;
        }

        public async Task<string> AddUser(UserDto newUser)
        {
            var dbUser = _mapper.Map<User>(newUser);
            _dataContext.User.Add(dbUser);
            var result = await _dataContext.SaveChangesAsync();
            if(result > 0)
            {
                return "success";
            }
            return "fail";
        }

        public async Task<string> UpdateUser(UserDto updateUser)
        {
            try
            {
                var dbUser = _dataContext.User.FirstOrDefault(x => x.Id == updateUser.Id && x.IsDelete == 0);
                if (dbUser is null)
                    throw new Exception($"User with Id '{updateUser.Id}' not found.");

                dbUser.UserName = updateUser.UserName;
                dbUser.Password = updateUser.Password;
                dbUser.Phone = updateUser.Phone;
                dbUser.UpdatedBy = 1;
                dbUser.UpdatedDate = DateTime.Now;
                _dataContext.User.Update(dbUser);
                var result = await _dataContext.SaveChangesAsync();
                if (result > 0)
                {
                    return "success";
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "fail";
        }

        public string CheckUser(LoginUserDto loginUser)
        {
            var errorMsg = "User not found.";
            var dbUser = _dataContext.User.FirstOrDefault(x => x.Phone == loginUser.Phone && x.IsDelete == 0);
            if(dbUser is null)
                return errorMsg;

            if (!BCrypt.Net.BCrypt.Verify(loginUser.Password, dbUser.Password))
                return errorMsg;

            return "Exist";
        }

        public async Task<UserDto> GetUserByPhone(LoginUserDto loginUser)
        {
            var dbUser = await _dataContext.User.FirstOrDefaultAsync(x => x.Phone == loginUser.Phone && x.IsDelete == 0);
            var user = _mapper.Map<UserDto>(dbUser);
            return user;
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            var dbToken = _dataContext.RefreshToken.FirstOrDefault(x=>x.UserId== refreshToken.UserId);
            if (dbToken is null)
            {
                _dataContext.RefreshToken.Add(refreshToken);
            }
            else
            {
                dbToken.UpdatedDate = DateTime.Now;
                dbToken.UpdatedBy= 1;
                dbToken.ExpiredDate = refreshToken.ExpiredDate;
                dbToken.Token = refreshToken.Token;
                _dataContext.RefreshToken.Update(dbToken);
            }
            _dataContext.SaveChanges();
        }

        public RefreshToken GetRefreshToken(string refreshToken)
        {
            var dbToken = _dataContext.RefreshToken.FirstOrDefault(x => x.Token == refreshToken && x.IsDelete == 0);
            if (dbToken is null)
                return new RefreshToken();
            return dbToken;
        }
    }
}
