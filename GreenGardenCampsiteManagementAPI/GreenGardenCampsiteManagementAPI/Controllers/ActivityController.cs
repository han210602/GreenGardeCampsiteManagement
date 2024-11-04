using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Accounts;
using Repositories.Activities;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private IActivityRepository _repo;
        public ActivityController(IActivityRepository repo )
        {
            _repo = repo;
        }
        [HttpGet("GetAllActivities")]
        public IActionResult GetAllActivities()
        {

            try
            {
                return Ok(_repo.GetAllActivities().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        } 
    }
}
