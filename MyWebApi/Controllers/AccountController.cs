using Microsoft.AspNetCore.Mvc;
using MyWebApi.Model;
using System.Threading.Tasks;
using MyWebApi.Service;
using MyWebApi.Response;
using MyWebApi.Definition;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        #region Create
        [HttpPost("CreateAccount")]
        public async Task<ActionResult<CreateAccountResponse>> CreateAccount(CreateAccountModel model)
        {
            var response = new CreateAccountResponse();

            var status = await _service.AllowCreate(model.Account, model.Password);
            if (status == AccountStatus.HadAccount)
            {
                response.Status = status;
                return response;
            }
            else if (status == AccountStatus.AccountTooShort)
            {
                response.Status = status;
                return response;
            }
            else if (status == AccountStatus.PasswordTooShort)
            {
                response.Status = status;
                return response;
            }

            response.Data = await _service.CreateAccount(model);

            return response;
            //return CreatedAtAction(nameof(GetAccountModel),
            //                       new
            //                       {
            //                           account = model.Account
            //                       }, ItemToDTO(account));
        }
        #endregion

        #region Read
        [HttpGet("ReadAccount")]
        public async Task<GetAccountResponse> GetAccountModel(string account)
        {
            var response = await _service.GetAccountModel(account);
            return response;
        }

        [HttpGet("ReadAccoutList")]
        public async Task<GetAccountListResponse> GetAccountList(bool containDelete)
        {
            var response = await _service.GetAccountList(containDelete);
            return response;
        }
        #endregion

        #region Update
        [HttpPut("SleepAccount")]
        public async Task<AccountSleepResponse> SleepAccount(string account)
        {
            var response = await _service.SleepAccount(account);
            return response;
        }

        [HttpPut("WakeUpAccount")]
        public async Task<AccountSleepResponse> WakeUpAccount(string account)
        {
            var response = await _service.WakeUpAccount(account);
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteAccount")]
        public async Task<DeleteAccountResponse> DeleteAccount(string account)
        {
            var response = await _service.DeleteAccount(account);
            return response;
        }
        #endregion

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> LoginAccount(LoginModel model)
        {
            var response = await _service.Login(model);
            
            return response;
        }
    }
}
