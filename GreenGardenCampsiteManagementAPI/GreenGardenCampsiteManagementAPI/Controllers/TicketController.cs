using AutoMapper;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Mvc;
using Repositories.CampingGear;
using Repositories.Tickets;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ITicketRepository _repo;
        public TicketController(ITicketRepository repo, IMapper mapper)
        {
            _repo = repo;
        }
        [HttpGet("GetAllTickets")]
        public IActionResult GetAllTickets()
        {
            try
            {

                var user = _repo.GetAllTickets().ToList();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetTicketDetail")]
        public IActionResult GetTicketDetail(int id)
        {
            try
            {

                var user = _repo.GetTicketDetail(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllTicketCategories")]
        public IActionResult GetAllTicketCategories()
        {
            try
            {

                var user = _repo.GetAllTicketCategories().ToList();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
        [HttpPost("AddTicket")]
        public IActionResult AddTicket([FromBody] AddTicket ticketDto)
        {
            if (ticketDto == null)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                _repo.AddTicket(ticketDto);
                return Ok("Ticket added successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateTicket")]
        public IActionResult UpdateTicket([FromBody] UpdateTicket ticketDto)
        {
            if (ticketDto == null)
            {
                return BadRequest("Invalid input.");
            }

            try
            {
                _repo.UpdateTicket(ticketDto);
                return Ok("Ticket updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetTicketsByCategoryAndSort")]
        public IActionResult GetTicketsByCategoryAndSort(int? categoryId, int? sort)
        {
            try
            {
                var tickets = _repo.GetTicketsByCategoryIdAndSort(categoryId, sort);
                if (tickets == null || tickets.Count == 0)
                {
                    return NotFound("No tickets found for the specified category ID.");
                }
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("DeleteTicket")]
        public IActionResult DeleteTicket(int? ticketId)
        {
            if (!ticketId.HasValue)
            {
                return BadRequest("ID sản phẩm không hợp lệ.");
            }

            try
            {
                var item = _repo.GetTicketDetail(ticketId.Value);
                if (item == null)
                {
                    return NotFound("Sản phẩm không tồn tại.");
                }

                var result = _repo.DeleteTicket(ticketId.Value);
                if (!result)
                {
                    return BadRequest("Không thể xóa sản phẩm này vì nó đang được sử dụng trong các combo hoặc đơn hàng.");
                }

                return Ok("Xóa sản phẩm thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
