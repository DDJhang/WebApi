using MyWebApi.Definition;
using MyWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApi.Repository.Interface
{
    public interface IPunchClockRepository
    {
        string CreateDB();
        Task Add(OneDayPunchModel model, string tableName);
        Task Update(OneDayPunchModel model, string tableName, PunchType type);
        Task<dynamic> GetPunchData(string account);
        Task<List<dynamic>> GetPunchListByAccount(string account, int days);
        Task<Dictionary<string, IEnumerable<dynamic>>> GetAllPunchList(int days);
        bool CheckTableExist(string tableName);
    }
}
