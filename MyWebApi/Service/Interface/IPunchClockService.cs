using MyWebApi.Definition;
using MyWebApi.Model;
using MyWebApi.Response.Punch;
using System.Threading.Tasks;

namespace MyWebApi.Service.Interface
{
    public interface IPunchClockService
    {
        Task<PunchResponse> PunchClock(PunchModel model);
        Task<AttendanceResponse> GetAttendance(AttendanceModel model);
        Task<AttendanceListResponse> GetAttendanceList(AttendanceStatus status); 
    }
}
