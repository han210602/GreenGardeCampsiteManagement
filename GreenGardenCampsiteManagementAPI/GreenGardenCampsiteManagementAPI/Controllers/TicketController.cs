﻿using AutoMapper;
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

                var user = _repo.GetAllCustomerTickets().ToList();
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
        public IActionResult GetTicketsByCategoryAndSort([FromQuery] int? categoryId, [FromQuery] int? sortBy, [FromQuery] int page = 1, [FromQuery] int pageSize = 3)
        {
            try
            {
                var (tickets, totalPages) = _repo.GetTicketsByCategoryIdAndSort(categoryId, sortBy, page, pageSize);

                // Create a response object to return both data and metadata
                var response = new
                {
                    TotalPages = totalPages,
                    CurrentPage = page,
                    PageSize = pageSize,
                    Data = tickets
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("ChangeTicketStatus")]
        public IActionResult ChangeGearStatus([FromQuery] int ticketId)
        {
            if (ticketId <= 0)
            {
                return BadRequest("Invalid item ID.");
            }


            try
            {
                // Check if the item exists
                var item = _repo.GetTicketDetail(ticketId);
                if (item == null)
                {
                    return NotFound($"Ticket item with ID {ticketId} does not exist.");
                }

                // Update the status
                _repo.ChangeTicketStatus(ticketId);
                return Ok($"Ticket item {ticketId} status changed.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
