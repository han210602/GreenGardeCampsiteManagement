using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.ComboFood;
using Repositories.Orders;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboFoodController : ControllerBase
    {
        private IComboFoodRepository _repo;
        public ComboFoodController(IComboFoodRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                return Ok(_repo.ComboFoods());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllCustomerOrders")]
        public IActionResult GetAllCustomerOrders()
        {
            try
            {
                return Ok(_repo.CustomerComboFoods());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetComboFoodDetail/{id}")]
        public IActionResult GetComboFoodDetail(int id)
        {
            try
            {
                return Ok(_repo.ComboFoodDetail(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("ChangeComboFoodStatus")]
        public IActionResult ChangeGearStatus([FromQuery] int comboId)
        {
            if (comboId <= 0)
            {
                return BadRequest("Invalid item ID.");
            }


            try
            {
                // Check if the item exists
                var item = _repo.ComboFoodDetail(comboId);
                if (item == null)
                {
                    return NotFound($"Food and drink item with ID {comboId} does not exist.");
                }

                // Update the status
                _repo.ChangeComboFoodStatus(comboId);
                return Ok($"Food and drink item {comboId} status changed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
