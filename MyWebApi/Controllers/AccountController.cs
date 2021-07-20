using Microsoft.AspNetCore.Mvc;
using MyWebApi.Model;
using System.Threading.Tasks;
using MyWebApi.Service;
using MyWebApi.Response;
using MyWebApi.Definition;
using MyWebApi.Observable;


namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private IAccountService _service;

        private ILogService _logService;

        public AccountController(IAccountService service, ILogService log, LogObserver logserver)
        {
            _service = service;
            _logService = log;

            _logService.Subscribe(logserver);
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
        }
        #endregion

        #region Read
        [HttpGet("ReadAccount")]
        public async Task<ActionResult<GetAccountResponse>> GetAccountModel(string account)
        {
            var response = await _service.GetAccountModel(account, false);
            return response;
        }

        [HttpGet("ReadAccoutList")]
        public async Task<ActionResult<GetAccountListResponse>> GetAccountList(bool containDelete)
        {
            var response = await _service.GetAccountList(containDelete);
            return response;
        }
        #endregion

        #region Update
        [HttpPut("SleepAccount")]
        public async Task<ActionResult<AccountSleepResponse>> SleepAccount(string account)
        {
            var response = await _service.SleepAccount(account);
            return response;
        }

        [HttpPut("WakeUpAccount")]
        public async Task<ActionResult<AccountSleepResponse>> WakeUpAccount(string account)
        {
            var response = await _service.WakeUpAccount(account);
            return response;
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteAccount")]
        public async Task<ActionResult<DeleteAccountResponse>> DeleteAccount(string account)
        {
            var response = await _service.DeleteAccount(account);
            return response;
        }
        #endregion

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> LoginAccount(LoginModel model)
        {
            var response = await _service.Login(model);

            if (string.IsNullOrEmpty(response.ErrorMsg))
            { 
                _logService.WriteLog(new LogMessage() 
                {
                    Type = LogType.LoginAccount,
                    Account = model.Account,
                    Time = Method.DateTimeToPunchString(System.DateTime.Now),
                    Message = LogType.LoginAccount.ToString()
                });
            }

            return response;
        }
    }
}
