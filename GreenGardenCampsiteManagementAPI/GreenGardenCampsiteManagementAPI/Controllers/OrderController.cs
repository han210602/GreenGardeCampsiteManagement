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
    public class OrderController : ControllerBase
    {
        private IOrderRepository _repo;
        public OrderController(IOrderRepository repo)
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
        [HttpPost("CreateUniqueOrder")]
        public IActionResult CreateUniqueOrder([FromBody] CreateUniqueOrderRequest order) 
        {
            try
            {
                return Ok(_repo.CreateUniqueOrder(order.Order,order.OrderTicket, order.OrderCampingGear, order.OrderFood, order.OrderFoodCombo));
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
    public class CreateUniqueOrderRequest
    {
        public OrderDTO Order { get; set; }
        public List<OrderTicketDetailDTO> OrderTicket { get; set; }
        public List<OrderCampingGearDetailDTO> OrderCampingGear { get; set; }
        public List<OrderFoodDetailDTO> OrderFood { get; set; }
        public List<OrderFoodComboDetailDTO> OrderFoodCombo { get; set; }
    }
}
