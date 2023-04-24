using EVoucherMgmtAPI.Data;
using EVoucherMgmtAPI.Dtos;
using EVoucherMgmtAPI.Models;
using EVoucherMgmtAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EVoucherMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService; 
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("Detail/{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            if(result == null)
                return NotFound("User not found.");
            return Ok(result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<string>> AddUser(UserDto newUser)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            newUser.Password = passwordHash;
            var response = await _userService.AddUser(newUser);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<string>> UpdateUser(UserDto updateUser)
        {
            var result = await _userService.UpdateUser(updateUser);
            if (result == null)
                return NotFound("User not found.");
            return Ok(result);
        }
    }
}
