using AutoMapper;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Repositories.Accounts;
using Repositories.CampingGear;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampingGearController : ControllerBase
    {
        private ICampingGearRepository _repo;
        public CampingGearController(ICampingGearRepository repo, IMapper mapper)
        {
            _repo = repo;
        }
        [HttpGet("GetAllCampingGears")]
        public IActionResult GetAllCampingGears()
        {
            try
            {
                var campingGears = _repo.GetAllCampingGears();
                return Ok(campingGears);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllCampingGearCategories")]
        public IActionResult GetAllCampingGearCategories()
        {
            try
            {
                var campingGears = _repo.GetAllCampingGearCategories();
                return Ok(campingGears);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddCampingGear")]
        public IActionResult AddCampingGear([FromBody] AddCampingGearDTO gearDto)
        {
            if (gearDto == null)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                _repo.AddCampingGear(gearDto);
                return Ok("Camping gear added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/CampingGear/UpdateCampingGear
        [HttpPut("UpdateCampingGear")]
        public IActionResult UpdateCampingGear([FromBody] UpdateCampingGearDTO gearDto)
        {
            if (gearDto == null)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                _repo.UpdateCampingGear(gearDto);
                return Ok("Camping gear updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetCampingGearsBySort")]
        public IActionResult GetCampingGearsBySort([FromQuery] int? categoryId, [FromQuery] int? sortBy, [FromQuery] int? priceRange, [FromQuery] int? popularity)
        {
            try
            {
                var campingGears = _repo.GetCampingGearsBySort(categoryId, sortBy, priceRange, popularity);
                return Ok(campingGears);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
