using Microsoft.AspNetCore.Mvc;
using MyWebApi.Definition;
using MyWebApi.Model;
using MyWebApi.Response.Punch;
using MyWebApi.Service.Interface;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PunchClockController : ControllerBase
    {
        private IPunchClockService _service;

        public PunchClockController(IPunchClockService service)
        {
            _service = service;
        }

        [HttpPost("Test")]
        public ActionResult<string> Test()
        {
            return "TEST!!!";
        }

        [HttpPost("CreatePunchTable")]
        public ActionResult<string> CrreateDB()
        {
            return _service.CreateDB(); ;
        }


        [HttpPost("PunchClock")]
        public async Task<ActionResult<PunchResponse>> PunchClock(PunchModel model)
        {
            return await _service.PunchClock(model);
        }

        [HttpGet("GetAttendance")]
        public async Task<ActionResult<AttendanceResponse>> GetAttendance(AttendanceModel model)
        {
            return await _service.GetAttendance(model);
        }

        [HttpGet("GetAttendanceList")]
        public async Task<ActionResult<AttendanceListResponse>> GetAttendanceList(AttendanceStatus status)
        {
            return await _service.GetAttendanceList(status);
        }
    }
}
