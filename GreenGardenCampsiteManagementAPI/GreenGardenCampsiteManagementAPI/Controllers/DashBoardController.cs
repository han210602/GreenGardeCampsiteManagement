using BusinessObject.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Activities;
using Repositories.DashBoard;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private IDashBoardRepository _repo;

        public DashBoardController(IDashBoardRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetProfit")]
        public IActionResult GetProfit()
        {
            try { 
           
                var item = new ProfitDTO()
                {
                    TotalAmount = _repo.TotalAmount(),
                    TotalOrderOnline = _repo.TotalOrderOnline(),
                    TotalDepositOrderOnline = _repo.TotalDepositOrderOnline(),
                    MoneyTotalDepositOrderOnline = _repo.MoneyTotalDepositOrderOnline(),
                    TotalOrderCancel = _repo.TotalOrderCancel(),
                    TotalDepositOrderCancel = _repo.TotalDepositOrderCancel(),
                    MoneyTotalDepositOrderCancel = _repo.MoneyTotalDepositOrderCancel(),
                    TotalOrderUsing = _repo.TotalOrderUsing(),
                    TotalDepositOrderUsing = _repo.TotalDepositOrderUsing(),
                    MoneyTotalDepositOrderUsing = _repo.MoneyTotalDepositOrderUsing(),
                    TotalOrderCheckout = _repo.TotalOrderCheckout(),
                    MoneyTotalAmountOrderCheckout = _repo.MoneyTotalAmountOrderCheckout(),
                };
                return Ok(item);
        }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
    }

}
        [HttpGet("GetListCustomer")]
        public IActionResult GetListCustomer()
        {
             try { 
            
                return Ok(_repo.ListCustomer());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }
    }
}
