using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Repositories.Combo;
using Repositories.Tickets;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComboController : ControllerBase
    {
        private IComboRepository _repo;
        public ComboController(IComboRepository repo, IMapper mapper)
        {
            _repo = repo;
        }
        [HttpGet("GetAllCombos")]
        public IActionResult GetAllCombos()
        {
            try
            {
                return Ok(_repo.Combos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("GetComboDetail/{id}")]
        public IActionResult GetComboDetail(int id)
        {
            try
            {
                return Ok(_repo.ComboDetail(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
