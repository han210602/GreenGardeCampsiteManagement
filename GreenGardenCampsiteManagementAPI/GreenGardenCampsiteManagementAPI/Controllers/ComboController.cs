using AutoMapper;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Repositories.Combo;
using Repositories.Tickets;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private IComboRepository _repo;
        public ComboController(IComboRepository repo, IMapper mapper)
        {
            _repo = repo;
        }
        [HttpGet("GetAllCombos")]
        public IActionResult GetAllCombos()
        {
            try
            {
                return Ok(_repo.Combos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetComboDetail/{id}")]
        public IActionResult GetComboDetail(int id)
        {
            try
            {
                return Ok(_repo.ComboDetail(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("AddCombo")]
        public IActionResult AddCombo([FromBody] AddCombo newCombo)
        {
            try
            {
                _repo.AddCombo(newCombo);
                return Ok("Combo đã được thêm thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Cập nhật combo
        [HttpPut("UpdateCombo")]
        public IActionResult UpdateCombo([FromBody] AddCombo updatedCombo)
        {
            try
            {
                // Kiểm tra xem ComboId có tồn tại không
                var existingCombo = _repo.ComboDetail(updatedCombo.ComboId);
                if (existingCombo == null)
                {
                    return NotFound("Combo không tồn tại.");
                }

                // Nếu combo tồn tại, tiến hành cập nhật
                _repo.UpdateCombo(updatedCombo);
                return Ok("Combo đã được cập nhật thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("ChangeComboStatus")]
        public IActionResult ChangeGearStatus([FromQuery] int comboId, [FromBody] ChangeComboStatus newStatus)
        {
            if (comboId <= 0)
            {
                return BadRequest("Invalid item ID.");
            }

            if (newStatus == null || newStatus.Status == null)
            {
                return BadRequest("Invalid status data.");
            }

            try
            {
                // Check if the item exists
                var item = _repo.ComboDetail(comboId);
                if (item == null)
                {
                    return NotFound($"Food and drink item with ID {comboId} does not exist.");
                }

                // Update the status
                _repo.ChangeComboStatus(comboId, newStatus);
                return Ok($"Food and drink item {comboId} status changed to {newStatus.Status.Value}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
