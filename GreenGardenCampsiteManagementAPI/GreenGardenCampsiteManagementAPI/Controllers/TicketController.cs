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
        [HttpGet("GetAllCustomerTickets")]
        public IActionResult GetAllCustomerTickets()
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
        [HttpPut("ChangeTicketStatus")]
        public IActionResult ChangeGearStatus([FromQuery] int ticketId, [FromBody] ChangeTicketStatus newStatus)
        {
            if (ticketId <= 0)
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
                var item = _repo.GetTicketDetail(ticketId);
                if (item == null)
                {
                    return NotFound($"Food and drink item with ID {ticketId} does not exist.");
                }

                // Update the status
                _repo.ChangeTicketStatus(ticketId, newStatus);
                return Ok($"Food and drink item {ticketId} status changed to {newStatus.Status.Value}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
