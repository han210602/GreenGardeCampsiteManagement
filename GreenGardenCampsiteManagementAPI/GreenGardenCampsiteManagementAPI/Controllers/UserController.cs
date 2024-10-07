using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repositories.Accounts;
using Repositories.IReponsitory;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private IUserReponsitory _repo;
        public UserController(IUserReponsitory repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            try
            {

                var user = _repo.GetAllUser().ToList();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
