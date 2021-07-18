using MyWebApi.Definition;
using MyWebApi.Model;
using MyWebApi.Repository;
using MyWebApi.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Service
{
    public class AccountService: IAccountService
    {
        private IAccountRepository _resp;

        public AccountService(IAccountRepository resp)
        {
            _resp = resp;
        }

        public async Task<AccountStatus> AllowCreate(string account, string password)
        {
            // 已有相同帳號
            var hadAccount = await _resp.GetPlayerByAccount(account);
            if (hadAccount != null)
                return AccountStatus.HadAccount;

            // 帳號過短
            if (account.Length < 5)
                return AccountStatus.AccountTooShort;

            // 密碼過短
            if (password.Length < 5)
                return AccountStatus.PasswordTooShort;

            return AccountStatus.Success;
        }

        public async Task<AccountModelDTO> CreateAccount(CreateAccountModel model)
        {
            var account = new AccountModel
            {
                Account = model.Account,
                Password = model.Password,
                Name = model.Name,
                Inactive = 0
            };

            await _resp.Add(account);

            return Method.AccountToDTO(account);
        }

        public async Task<DeleteAccountResponse> DeleteAccount(string account)
        {
            var response = new DeleteAccountResponse()
            {
                Status = AccountStatus.Success
            };

            var result = await GetAccountModel(account);
            if (result.Status != AccountStatus.Success)
            { 
                response.Status = AccountStatus.InvalidAccount;
                return response;
            }

            await _resp.DeleteByAccount(account);

            return response;
        }

        public async Task<GetAccountListResponse> GetAccountList(bool containDelete)
        {
            IEnumerable<dynamic> playerList = await _resp.GetPlayerList(containDelete);

            var response = new GetAccountListResponse()
            {
                Status = AccountStatus.Success,
                Accounts = playerList.Select(x => Method.AccountToDTO((AccountModel)x)).ToArray()
            };

            return response;
        }

        public async Task<GetAccountResponse> GetAccountModel(string account, bool isTracking = true)
        {
            var response = new GetAccountResponse()
            {
                Status = AccountStatus.Success,
                Account = null
            };

            var model = await _resp.GetPlayerByAccount(account, isTracking);
            if (model == null)
            {
                response.Status = AccountStatus.InvalidAccount;
                return response;
            }
            else
            {
                response.Account = Method.AccountToDTO(model);
                return response;
            }
        }

        public async Task<LoginResponse> Login(LoginModel model)
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
                return response;
            }

            if (account.Password != model.Password)
            {
                response.ErrorMsg = "Password Is WRONG!!";
                return response;
            }

            response.Data = new AccountModelDTO()
            {
                Account = account.Account,
                Name = account.Name
            };


            return response;
        }

        public async Task<AccountSleepResponse> SleepAccount(string account)
        {
            var response = new AccountSleepResponse()
            {
                Status = AccountStatus.Success,
                Account = null
            };

            var model = await _resp.GetPlayerByAccount(account);
            if (model == null)
            {
                response.Status = AccountStatus.InvalidAccount;
                return response;
            }

            model.Inactive = 1;

            await _resp.UpdateInActive(model);

            response.Account = Method.AccountToDTO(model);

            return response;
        }

        public async Task<AccountSleepResponse> WakeUpAccount(string account)
        {
            var response = new AccountSleepResponse()
            {
                Status = AccountStatus.Success,
                Account = null
            };

            var model = await _resp.GetPlayerByAccount(account);
            if (model == null)
            {
                response.Status = AccountStatus.InvalidAccount;
                return response;
            }

            model.Inactive = 0;

            await _resp.UpdateInActive(model);

            response.Account = Method.AccountToDTO(model);

            return response;
        }
    }
}
