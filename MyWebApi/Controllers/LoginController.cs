using Microsoft.AspNetCore.Mvc;
using MyWebApi.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using MyWebApi.Repository;
using MyWebApi.Service;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IRespository _resp;

        private LoginService service;

        public LoginController(IRespository resp)
        {
            _resp = resp;
            service = new LoginService(resp);
        }

        #region Create
        [HttpPost("CreateAccount")]
        public async Task<ActionResult<CreateAccountResponse>> CreateAccount(CreateAccountModel model)
        {
            var response = new CreateAccountResponse();

            var status = await service.AllowCreate(model.Account, model.Password);
            if (status == CreateAccountStatus.HadAccount)
            {
                response.Status = status;
                return response;
            }
            else if (status == CreateAccountStatus.AccountTooShort)
            {
                response.Status = status;
                return response;
            }
            else if (status == CreateAccountStatus.PasswordTooShort)
            {
                response.Status = status;
                return response;
            }

            var account = new AccountModel
            {
                Account = model.Account,
                Password = model.Password,
                Name = model.Name,
                Money = 0,
                Delete = 0
            };

            _resp.Add(account);
            await _resp.SaveChangesAsync();

            response.Data = ItemToDTO(account);
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
        public async Task<ActionResult<AccountModelDTO>> GetAccountModel(string account)
        {
            var model = await _resp.GetPlayerByAccount(account);
            if (model != null)
            {
                var modelDTO = ItemToDTO(model);
                return modelDTO;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("ReadAccoutList")]
        public async Task<ActionResult<IEnumerable<AccountModelDTO>>> GetAccountList()
        {
            IEnumerable<AccountModel> playerList = await _resp.GetPlayerList(false);
            return playerList.Select(x => ItemToDTO(x)).ToList();
        }
        #endregion

        #region Update
        [HttpPut("UpdateMoney")]
        public async Task<ActionResult<AccountModelDTO>> UpdateAccountMoney(UpdateAccountModel account)
        {
            var model = await _resp.GetPlayerByAccount(account.Account);
            model.Money = account.Money;

            await _resp.SaveChangesAsync();

            return ItemToDTO(model);
        }

        [HttpPut("UpdateName")]
        public async Task<ActionResult<AccountModelDTO>> UpdateAccountName(UpdateNameModel account)
        {
            var model = await _resp.GetPlayerByAccount(account.Account);
            model.Name = account.Name;

            await _resp.SaveChangesAsync();

            return ItemToDTO(model);
        }

        [HttpPut("SleepAccount")]
        public async Task<ActionResult<AccountModelDTO>> SleepAccount(string account)
        {
            var model = await _resp.GetPlayerByAccount(account);
            model.Delete = 1;

            await _resp.SaveChangesAsync();

            return ItemToDTO(model);
        }

        [HttpPut("WakeUpAccount")]
        public async Task<ActionResult<AccountModelDTO>> WakeUpAccount(string account)
        {
            var model = await _resp.GetPlayerByAccount(account);
            model.Delete = 0;

            await _resp.SaveChangesAsync();

            return ItemToDTO(model);
        }
        #endregion

        #region Delete
        [HttpDelete("DeleteAccount")]
        public async Task<ActionResult<string>> DeleteAccount(string account)
        {
            await _resp.DeleteByAccount(account);
            await _resp.SaveChangesAsync();

            return "Delete DB Account: " + account;
        }
        #endregion

        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginAccount(LoginModel model)
        {
            var response = new LoginResponse()
            {
                ErrorMsg = "",
                Data = null
            };

            var account = await _resp.GetPlayerByAccount(model.Account);
            if (account == null)
            {
                response.ErrorMsg = "Invalid Account";
                return JsonSerializer.Serialize(response);
            }

            if (account.Password != model.Password)
            {
                response.ErrorMsg = "Password Is WRONG!!";
                return JsonSerializer.Serialize(response);
            }

            response.Data = ItemToDTO(account);
            return JsonSerializer.Serialize(response);
        }

        private static LoginResponse CreateLoginResponse(LoginResponse response)
        {
            return response;
        }

        private static AccountModelDTO ItemToDTO(AccountModel model)
        {
            var dto = new AccountModelDTO()
            {
                Account = model.Account,
                Name = model.Name,
                Money = model.Money
            };

            return dto;
        }
    }
}
