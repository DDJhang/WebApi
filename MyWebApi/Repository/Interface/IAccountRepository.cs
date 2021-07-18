using MyWebApi.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApi.Repository
{
    public interface IAccountRepository
    {
        Task Add(AccountModel model);
        Task UpdateInActive(AccountModel model);
        Task Delete(AccountModel model);
        Task DeleteByAccount(string account);
        Task<IEnumerable<dynamic>> GetPlayerList(bool containDelete);
        Task<dynamic> GetPlayerByAccount(string account, bool isTracking = true);
    }
}
