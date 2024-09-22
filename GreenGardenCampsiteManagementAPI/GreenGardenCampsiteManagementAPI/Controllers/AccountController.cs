using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Accounts;

namespace GreenGardenCampsiteManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepository _repo;
        public AccountController(IAccountRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAllAccounts()
        {
            return _repo.GetAllAccount();
        }
        [HttpPost("Register")]
        public ActionResult<Account> Register(Account request)
        {
            Account a = new Account
            {
                AccountId=request.AccountId,
                Email = request.Email,
                Password = request.Password,
                Fullname = request.Fullname,
                
            };
            _repo.CreateAccount(a);
            return NoContent();
        }
    }
}
