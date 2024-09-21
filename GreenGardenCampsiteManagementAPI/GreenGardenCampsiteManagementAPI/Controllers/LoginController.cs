using GreenGardenCampsiteManagementAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // Danh sách người dùng mẫu
        private readonly List<User> _users = new List<User>
        {
            new User {Userid = 1, Username = "admin", Password = "1", Role = "Admin" },
            new User {Userid = 2, Username = "user", Password = "1", Role = "User" }
        };

        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // API đăng nhập
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var user = _users.SingleOrDefault(u => u.Username == loginRequest.Username
                                               && u.Password == loginRequest.Password);
            if (user != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("Userid", user.Userid.ToString()),
                    new Claim("UserName", user.Username),
                     new Claim(ClaimTypes.Role, user.Role)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn);

                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = tokenValue, User = user });
            }

            return NoContent();
        }
        [Authorize(Roles = "Admin")]
        // API lấy tất cả người dùng
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            return Ok(_users);
        }
    }
}
