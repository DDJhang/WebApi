using Microsoft.AspNetCore.Mvc;
using MyWebApi.Definition;
using MyWebApi.Model;
using MyWebApi.Response.Punch;
using System;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PunchClockController : ControllerBase
    {
        public PunchClockController()
        {
            // TODO......
        }

        [HttpPost("PunchIn")]
        public async Task<ActionResult<PunchResponse>> PunchIn(PunchModel model)
        {
            var response = new PunchResponse()
            {
                Message = "打卡失敗..."
            };

            if (model.Status == PunchStatus.PunchOut)
                return response;

            var curTick = DateTime.Now.Ticks;

            // TODO......

            response.Message = "打卡成功..." + DateTime.Now.ToString();
            return response;
        }

        [HttpPost("PunchOut")]
        public async Task<ActionResult<PunchResponse>> PunchOut(PunchModel model)
        {
            var response = new PunchResponse()
            {
                Message = "打卡失敗..."
            };

            if (model.Status == PunchStatus.PunchIn)
                return response;

            var curTick = DateTime.Now.Ticks;

            // TODO......

            response.Message = "打卡成功..." + DateTime.Now.ToString();
            return response;
        }

        [HttpGet("GetAttendance")]
        public async Task<ActionResult<AttendanceResponse>> GetAttendance(AttendanceModel model)
        {
            var response = new AttendanceResponse()
            {
                Message = "獲取失敗...",
                Data = null
            };

            // TODO......

            return response;
        }
    }
}
