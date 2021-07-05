using MyWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApi.Repository
{
    public interface IRespository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task DeleteByAccount(string account);
        Task<IEnumerable<AccountModel>> GetPlayerList(bool containDelete);
        Task<AccountModel> GetPlayerByAccount(string account);

        Task<bool> SaveChangesAsync();
    }
}
