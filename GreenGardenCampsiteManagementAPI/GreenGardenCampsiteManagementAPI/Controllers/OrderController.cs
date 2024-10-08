using AutoMapper;
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
        [HttpGet]
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


    }
}
