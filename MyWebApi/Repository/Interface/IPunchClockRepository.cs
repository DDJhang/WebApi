using MyWebApi.Definition;
using MyWebApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWebApi.Repository.Interface
{
    public interface IPunchClockRepository: IRepository
    {
        Task<OneDayPunchModel> GetPunchData(string account);
        Task<List<OneDayPunchModel>> GetPunchListByAccount(string account, AttendanceStatus status);
        Task<Dictionary<string, IEnumerable<OneDayPunchModel>>> GetAllPunchList(AttendanceStatus status);
    }
}
