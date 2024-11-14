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
        [HttpGet("GetAllCustomerCampingGears")]
        public IActionResult GetAllCustomerCampingGears()
        {
            try
            {
                var campingGears = _repo.GetAllCustomerCampingGears();
                return Ok(campingGears);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetCampingGearDetail")]
        public IActionResult GetCampingGearDetail(int id)
        {
            try
            {
                var campingGears = _repo.GetCampingGearDetail(id);
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
        [HttpPut("ChangeGearStatus")]
        public IActionResult ChangeGearStatus([FromQuery] int gearId)
        {
            if (gearId <= 0)
            {
                return BadRequest("Invalid item ID.");
            }

            try
            {
                // Check if the item exists
                var item = _repo.GetCampingGearDetail(gearId);
                if (item == null)
                {
                    return NotFound($"Gear item with ID {gearId} does not exist.");
                }

                // Update the status
                _repo.ChangeGearStatus(gearId);
                return Ok($"Gear item {gearId} status changed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
