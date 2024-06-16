using AuthenticationApi.Infrastructure;
using AuthenticationClassLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsRepository<Account, long> _repository;

        public AccountController(IAccountsRepository<Account, long> repository)
        {
            _repository = repository;
        }

        // GET: api/Accounts
        [HttpGet(template: "{CustId}")]
        public async Task<IActionResult> GetAllAccountsByCustomerId(int CustId)
        {
            var model = _repository.GetAllAccountsByCustomerID(CustId);
            return Ok(model);
        }

        // GET: api/Accontss/5
        [HttpGet(template: "AccId:{AccId}")]
        public async Task<ActionResult<Account>> GetAccountByAccountId(long AccId)
        {
            var model = _repository.GetAccountByAccountID(AccId);
            if (model is not null)
            {
                return model;
            }
            else
                return NotFound();
        }

        [HttpPost(template: "Create")]
        public ActionResult<Account> CreateAccount(Account model)
        {
            _repository.CreateAccount(model);
            return model;
        }

        [HttpDelete(template: "Delete")]
        public async Task<ActionResult<Account>> DeleteAccount(long AccId)
        {
            _repository.DeleteAccountByAccountId(AccId);
            return Ok();
        }
    }
}
