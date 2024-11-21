using BusinessObject.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize("AdminPolicy")]

        [HttpGet("GetProfit/{Month}")]
        public IActionResult GetProfit(string Month)
        {
            try { 
           
                
                return Ok(_repo.Profit(Month));
                }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
        [Authorize("AdminPolicy")]

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
