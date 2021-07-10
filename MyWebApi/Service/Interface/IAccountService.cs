using MyWebApi.Definition;
using MyWebApi.Model;
using MyWebApi.Response;
using System.Threading.Tasks;

namespace MyWebApi.Service
{
    public interface IAccountService
    {
        Task<AccountStatus> AllowCreate(string account, string password);
        Task<AccountModelDTO> CreateAccount(CreateAccountModel model);
        Task<GetAccountResponse> GetAccountModel(string account);
        Task<GetAccountListResponse> GetAccountList(bool containDelete);
        Task<AccountSleepResponse> SleepAccount(string account);
        Task<AccountSleepResponse> WakeUpAccount(string account);
        Task<DeleteAccountResponse> DeleteAccount(string account);
        Task<LoginResponse> Login(LoginModel model);
    }
}
