using MyWebApi.Definition;
using MyWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApi.Repository.Interface
{
    public interface IPunchClockRepository
    {
        Task Add(OneDayPunchModel model, string tableName);
        Task Update(OneDayPunchModel model, string tableName, PunchType type);
        Task<OneDayPunchModel> GetPunchData(string account);
        Task<List<OneDayPunchModel>> GetPunchListByAccount(string account, int days);
        Task<Dictionary<string, IEnumerable<OneDayPunchModel>>> GetAllPunchList(int days);
    }
}
