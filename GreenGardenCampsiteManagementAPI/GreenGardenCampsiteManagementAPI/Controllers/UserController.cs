using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Repositories.Users;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UserController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var users = _repo.GetAllUsers(); // Fetch users directly
                return Ok(users); // Return users directly
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            try
            {
                var employees = _repo.GetAllEmployees(); // Fetch employees directly
                return Ok(employees); // Return employees directly
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _repo.GetAllCustomers(); // Fetch customers directly
                return Ok(customers); // Return customers directly
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetUserById/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            try
            {
                var user = _repo.GetUserById(userId);
                if (user == null)
                {
                    return NotFound($"User with ID {userId} not found.");
                }
                return Ok(user); // Return user directly
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromBody] AddUserDTO newEmployeeDto)
        {
            if (newEmployeeDto == null)
            {
                return BadRequest("Employee data is null.");
            }

            try
            {
                bool result = _repo.AddEmployee(newEmployeeDto, null); // Pass IConfiguration if needed
                if (!result)
                {
                    return BadRequest("Tạo tài khoản thất bại. Email mày đã được sử dụng hoặc không đúng định dạng.");
                }
                return Ok("Employee added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer([FromBody] AddUserDTO newCustomerDto)
        {
            if (newCustomerDto == null)
            {
                return BadRequest("Customer data is null.");
            }

            try
            {
                bool result = _repo.AddCustomer(newCustomerDto, null); // Pass IConfiguration if needed
                if (!result)
                {
                    return BadRequest("Failed to add customer. The email may already be in use or invalid data was provided.");
                }
                return Ok("Customer added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO updatedUserDto)
        {
            if (updatedUserDto == null)
            {
                return BadRequest("User data is null.");
            }

            try
            {
                bool result = _repo.UpdateUser(updatedUserDto);
                if (!result)
                {
                    return BadRequest("Failed to update user.");
                }
                return Ok("User updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteUser/{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            try
            {
                bool result = _repo.DeleteUser(userId, null); // Pass IConfiguration if needed
                if (!result)
                {
                    return BadRequest("Failed to delete user.");
                }
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("BlockUser/{userId}")]
        public IActionResult BlockUser(int userId)
        {
            try
            {
                bool result = _repo.BlockUser(userId, null); // Pass IConfiguration if needed
                if (!result)
                {
                    return BadRequest("Failed to block user.");
                }
                return Ok("User blocked successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("UnBlockUser/{userId}")]
        public IActionResult UnBlockUser(int userId)
        {
            try
            {
                bool result = _repo.UnBlockUser(userId, null); // Pass IConfiguration if needed
                if (!result)
                {
                    return BadRequest("Failed to unblock user.");
                }
                return Ok("User unblock successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
