using EVoucherMgmtAPI.Dtos;
using EVoucherMgmtAPI.Models;
using EVoucherMgmtAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EVoucherMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login(LoginUserDto loginUser)
        {
            var response = _userService.CheckUser(loginUser);
            if(response != "Exist")
            {
                return BadRequest(response);
            }
            var user = await _userService.GetUserByPhone(loginUser);
            string token = CreateToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);
            SetRefreshToken(refreshToken);
            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken(int userId)
        {
            var refreshToken = new RefreshToken
            {
                UserId= userId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiredDate = DateTime.Now.AddDays(7)
            };
            _userService.AddRefreshToken(refreshToken);
            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.ExpiredDate
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }

        [HttpPost("RefreshToken")]
        public  async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var dbToken = _userService.GetRefreshToken(refreshToken);

            if (dbToken.Id == 0)
                return Unauthorized("Invalid Refresh Token.");

            if(dbToken.ExpiredDate < DateTime.Now)
                return Unauthorized("Token Expired.");

            var user = await _userService.GetUserById(dbToken.UserId);
            string jwtToken = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken(user.Id);
            SetRefreshToken(newRefreshToken);
            return Ok(jwtToken);
        }

        private string CreateToken(UserDto user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Phone", user.Phone)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtConfig:Secret").Value!));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
