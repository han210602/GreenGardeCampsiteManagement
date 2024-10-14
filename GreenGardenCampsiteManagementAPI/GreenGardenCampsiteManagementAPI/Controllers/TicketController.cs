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
        [HttpGet("GetTicketsByCategory/{categoryId}")] 
        public IActionResult GetTicketsByCategory(int categoryId)
        {
            try
            {
                var tickets = _repo.GetTicketsByCategoryId(categoryId);
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
    }
}
