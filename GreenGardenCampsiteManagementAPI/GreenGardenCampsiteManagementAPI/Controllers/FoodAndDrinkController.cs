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
        [HttpGet("GetAllCustomerFoodAndDrink")]
        public IActionResult GetAllCustomerFoodAndDrink()
        {
            try
            {
                var items = _repo.GetAllCustomerFoodAndDrink();
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
        public IActionResult GetFoodAndDrinksBySort([FromQuery] int? categoryId, [FromQuery] int? sortBy, [FromQuery] int? priceRange, [FromQuery] int page = 1, [FromQuery] int pageSize = 6)
        {
            try
            {
                // Call the repository method that returns both the list and the total pages
                var (foodAndDrinks, totalPages) = _repo.GetFoodAndDrinks(categoryId, sortBy, priceRange, page, pageSize);

                // Create a response object to return both data and metadata
                var response = new
                {
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize,
                    Data = foodAndDrinks
                };

                return Ok(response);
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
        [HttpPut("ChangeFoodStatus")]
        public IActionResult ChangeFoodStatus([FromQuery] int itemId)
        {
            if (itemId <= 0)
            {
                return BadRequest("Invalid item ID.");
            }
            try
            {
                // Check if the item exists
                var item = _repo.GetFoodAndDrinkDetail(itemId);
                if (item == null)
                {
                    return NotFound($"Food and drink item with ID {itemId} does not exist.");
                }

                // Update the status
                _repo.ChangeFoodStatus(itemId);
                return Ok($"Food and drink item {itemId} status changed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
