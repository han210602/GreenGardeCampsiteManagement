using Microsoft.AspNetCore.Mvc;
using Repositories.Events;
using BusinessObject.DTOs;
using System;
using System.Threading.Tasks;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _repo;

        public EventController(IEventRepository repo)
        {
            _repo = repo;
        }

        // Lấy tất cả các sự kiện
        [HttpGet("GetAllEvents")]
        public IActionResult GetAllEvents()
        {
            try
            {
                var events = _repo.GetAllEvents();
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // Thêm một sự kiện mới
        [HttpPost("AddEvent")]
        public async Task<IActionResult> AddEvent([FromBody] CreateEventDTO eventDTO)
        {
            if (eventDTO == null)
            {
                return BadRequest("Thông tin sự kiện không hợp lệ.");
            }

            try
            {
                var result = await _repo.AddEvent(eventDTO,null);
                if (result)
                {
                    return Ok("Thêm sự kiện thành công.");
                }
                else
                {
                    return StatusCode(500, "Không thể thêm sự kiện.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // Cập nhật thông tin một sự kiện
        [HttpPut("UpdateEvent")]
        public IActionResult UpdateEvent([FromBody] UpdateEventDTO eventDTO)
        {
            if (eventDTO == null || eventDTO.EventId <= 0)
            {
                return BadRequest("Thông tin sự kiện không hợp lệ.");
            }

            try
            {
                var result = _repo.UpdateEvent(eventDTO);
                if (result)
                {
                    return Ok("Cập nhật sự kiện thành công.");
                }
                else
                {
                    return NotFound("Không tìm thấy sự kiện.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // Xóa một sự kiện
        [HttpDelete("DeleteEvent/{eventId}")]
        public IActionResult DeleteEvent(int eventId)
        {
            if (eventId <= 0)
            {
                return BadRequest("ID sự kiện không hợp lệ.");
            }

            try
            {
                var result = _repo.DeleteEvent(eventId);
                if (result)
                {
                    return Ok("Xóa sự kiện thành công.");
                }
                else
                {
                    return NotFound("Không tìm thấy sự kiện.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
