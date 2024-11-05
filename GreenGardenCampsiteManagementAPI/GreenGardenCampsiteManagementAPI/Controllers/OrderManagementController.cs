using AutoMapper;
using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize("AdminAndEmployeePolicy")]

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
        [Authorize("AdminAndEmployeePolicy")]
        [HttpGet("GetAllOrderOnline")]
        public IActionResult GetAllOrderOnline()
        {
            try
            {
                return Ok(_repo.GetAllOrderOnline().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize("CustomerPolicy")]
        [HttpGet("GetCustomerOrders")]
        public IActionResult GetCustomerOrders(int customerId, bool? statusOrder, int? activityId)
        {
            try
            {
                return Ok(_repo.GetCustomerOrders(customerId, statusOrder, activityId).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize("CustomerPolicy")]
        [HttpPost("ChangeCustomerActivity")]
        public IActionResult ChangeCustomerActivity(int orderId)
        {
            try
            {
                _repo.UpdateActivity(orderId);
                return Ok("Activity updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize("CustomerPolicy")]
        [HttpPost("CheckOut")]
        public IActionResult CheckOut([FromBody] CheckOut order)
        {
            try
            {
                return Ok(_repo.CheckOut(order));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize("CustomerPolicy")]
        [HttpPost("CheckOutComboOrder")]
        public IActionResult CheckOutComboOrder([FromBody] CheckoutCombo order)
        {
            try
            {
                return Ok(_repo.CheckOutComboOrder(order));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetAllOrderDepositAndUsing")]
        public IActionResult GetAllOrderDepositAndUsing()
        {
            try
            {
                return Ok(_repo.GetAllOrderDepositAndUsing().ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize("AdminAndEmployeePolicy")]

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
        [Authorize("AdminAndEmployeePolicy")]

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


        [Authorize("AdminAndEmployeePolicy")]

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
        [Authorize("AdminAndEmployeePolicy")]

        [HttpPost("UpdateTicket")]
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
        [Authorize("AdminAndEmployeePolicy")]

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
        [Authorize("AdminAndEmployeePolicy")]

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
        [Authorize("AdminAndEmployeePolicy")]

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
        [Authorize("AdminAndEmployeePolicy")]

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
        [Authorize("AdminAndEmployeePolicy")]

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
        [Authorize("CustomerPolicy")]
        [HttpGet("GetCustomerOrderDetail/{id}")]
        public IActionResult GetCustomerOrderDetail(int id)
        {
            try
            {
                return Ok(_repo.GetCustomerOrderDetail(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize("AdminPolicy")]

        [HttpPut("EnterDeposit/{id}/{money}")]
        public IActionResult EnterDeposit(int id, decimal money)
        {
            try
            {
                return Ok(_repo.EnterDeposit(id, money));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [Authorize("AdminAndEmployeePolicy")]

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
        [Authorize("AdminPolicy")]

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
        [Authorize("AdminPolicy")]

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
        [Authorize("AdminAndEmployeePolicy")]

        [HttpPost("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] UpdateOrderDTO order)
        {
            try
            {
                return Ok(_repo.UpdateOrder(order));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

}
