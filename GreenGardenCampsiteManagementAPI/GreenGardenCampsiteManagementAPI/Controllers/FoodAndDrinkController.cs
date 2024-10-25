using AutoMapper;
using BusinessObject.DTOs;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.CampingGear;
using Repositories.FoodAndDrink;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodAndDrinkController : ControllerBase
    {
        private IFoodAndDrinkRepository _repo;
        public FoodAndDrinkController(IFoodAndDrinkRepository repo, IMapper mapper)
        {
            _repo = repo;
        }
        [HttpGet("GetAllFoodAndDrink")]
        public IActionResult GetAllFoodAndDrink()
        {
            try
            {
                var items = _repo.GetAllFoodAndDrink();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetFoodAndDrinkDetail")]
        public IActionResult GetFoodAndDrinkDetail(int itemId)
        {
            try
            {
                var items = _repo.GetFoodAndDrinkDetail(itemId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllFoodAndDrinkCategories")]
        public IActionResult GetAllFoodAndDrinkCategories()
        {
            try
            {
                var items = _repo.GetAllFoodAndDrinkCategories();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetFoodAndDrinksBySort")]
        public IActionResult GetFoodAndDrinksBySort([FromQuery] int? categoryId, [FromQuery] int? sortBy, [FromQuery] int? priceRange)
        {
            try
            {
                // Gọi đến Repository để lấy danh sách món ăn và đồ uống với các tiêu chí lọc
                var foodAndDrinks = _repo.GetFoodAndDrinks(categoryId, sortBy, priceRange);
                return Ok(foodAndDrinks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddFoodOrDrink")]
        public async Task<IActionResult> AddFoodOrDrink([FromBody] AddFoodOrDrinkDTO itemDto)
        {
            if (itemDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                _repo.AddFoodOrDrink(itemDto);
                return CreatedAtAction(nameof(GetAllFoodAndDrink), new { id = itemDto.ItemId }, itemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateFoodOrDrink")]
        public async Task<IActionResult> UpdateFoodOrDrink([FromBody] UpdateFoodOrDrinkDTO itemDto)
        {
            if (itemDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                _repo.UpdateFoodOrDrink(itemDto);
                return Ok("Food and Drink updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
