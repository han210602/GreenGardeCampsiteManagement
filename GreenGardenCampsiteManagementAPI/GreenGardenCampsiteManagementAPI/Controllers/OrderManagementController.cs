using AutoMapper;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Accounts;
using Repositories.Orders;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderManagementController : ControllerBase
    {
        private IOrderManagementRepository _repo;
        public OrderManagementController(IOrderManagementRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                return Ok(_repo.GetAllOrders().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetListOrderGearByUsageDate/{usagedate}")]
        public IActionResult GetListOrderGearByUsageDate(DateTime usagedate)
        {
            try
            {
                return Ok(_repo.GetListOrderGearByUsageDate(usagedate).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("CreateUniqueOrder")]
        public IActionResult CreateUniqueOrder([FromBody] CreateUniqueOrderRequest order) 
        {
            try
            {
                return Ok(_repo.CreateUniqueOrder(order));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("CreateComboOrder")]
        public IActionResult CreateComboOrder([FromBody] CreateComboOrderRequest order)
        {
            try
            {
                return Ok(_repo.CreateComboOrder(order));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateTicket")]
        public IActionResult UpdateTicket([FromBody] List<OrderTicketAddlDTO> ticket)
        {
            try
            {
                return Ok(_repo.UpdateTicket(ticket));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateCampingGear")]
        public IActionResult UpdateCampingGear([FromBody] List<OrderCampingGearAddDTO> ticket)
        {
            try
            {
                return Ok(_repo.UpdateGear(ticket));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateFoodAndDrink")]
        public IActionResult UpdateFoodAndDrink([FromBody] List<OrderFoodAddDTO> ticket)
        {
            try
            {
                return Ok(_repo.UpdateFood(ticket));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateFoodCombo")]
        public IActionResult UpdateFoodCombo([FromBody] List<OrderFoodComboAddDTO> ticket)
        {
            try
            {
                return Ok(_repo.UpdateComboFood(ticket));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateCombo")]
        public IActionResult UpdateCombo([FromBody] List<OrderComboAddDTO> ticket)
        {
            try
            {
                return Ok(_repo.UpdateCombo(ticket));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetOrderDetail/{id}")]
        public IActionResult GetOrderDetail(int id)
        {
            try
            {
                return Ok(_repo.GetOrderDetail(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("EnterDeposit/{id}/{money}")]
        public IActionResult EnterDeposit(int id,decimal money)
        {
            try
            {
                return Ok(_repo.EnterDeposit(id,money));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("UpdateActivityOrder/{idorder}/{idactivity}")]
        public IActionResult UpdateActivityOrder(int idorder, int idactivity)
        {
            try
            {
                return Ok(_repo.UpdateActivityOrder(idorder, idactivity));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("CancelDeposit/{id}")]
        public IActionResult CancelDeposit(int id)
        {
            try
            {
                return Ok(_repo.CancelDeposit(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("DeleteOrder/{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                return Ok(_repo.DeleteOrder(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
    
}
