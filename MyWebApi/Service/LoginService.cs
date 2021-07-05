using MyWebApi.Repository;
using System.Threading.Tasks;

namespace MyWebApi.Service
{
    public enum CreateAccountStatus
    { 
        Allow,
        HadAccount,
        AccountTooShort,
        PasswordTooShort
    }

    public class LoginService
    {
        private IRespository _resp;

        public LoginService(IRespository resp)
        {
            _resp = resp;
        }

        public async Task<CreateAccountStatus> AllowCreate(string account, string password)
        {
            // 已有相同帳號
            var hadAccount = await _resp.GetPlayerByAccount(account);
            if (hadAccount != null)
                return CreateAccountStatus.HadAccount;

            // 帳號過短
            if (account.Length < 5)
                return CreateAccountStatus.AccountTooShort;

            // 密碼過短
            if (password.Length < 5)
                return CreateAccountStatus.PasswordTooShort;

            return CreateAccountStatus.Allow;
        }
    }
}
