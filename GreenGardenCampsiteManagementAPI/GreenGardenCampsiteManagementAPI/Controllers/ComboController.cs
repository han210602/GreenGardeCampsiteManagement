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
    }
}
