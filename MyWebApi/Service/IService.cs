using System.Threading.Tasks;

namespace MyWebApi.Service
{
    public interface IService
    {
        Task<CreateAccountStatus> AllowCreate(string account, string password);
    }
}
