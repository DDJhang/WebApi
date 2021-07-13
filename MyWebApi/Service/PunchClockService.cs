using MyWebApi.Model;
using MyWebApi.Repository.Interface;
using MyWebApi.Response.Punch;
using MyWebApi.Service.Interface;
using System;
using System.Threading.Tasks;

namespace MyWebApi.Service
{
    public class PunchClockService : IPunchClockService
    {
        private IPunchClockRepository _rep;

        public PunchClockService(IPunchClockRepository rep)
        {
            _rep = rep;
        }

        public async Task<PunchResponse> PunchClock(PunchModel model)
        {
            var punch = await _rep.GetPunchData(model.Account);

            string time = Method.DateTimeToTableName(DateTime.Now);
            if (punch == null)
            {
                var punchModel = new OneDayPunchModel()
                {
                    Account = model.Account,
                    Name = model.Name,
                    PunchIn = time,
                    PunchOut = ""
                };
                _rep.Add(punchModel);
                await _rep.SaveChangesAsync();
            }
            else
            {
                punch.PunchOut = time;
                await _rep.SaveChangesAsync();
            }

            var response = new PunchResponse()
            {
                Message = "打卡上班成功... => " + time
            };

            return response;
        }

        public async Task<AttendanceResponse> GetAttendance(AttendanceModel model)
        {
            var punch = await _rep.GetPunchListByAccount(model.Account, model.Status);

            var response = new AttendanceResponse()
            {
                Message = "FAIL.....",
                Data = null
            };

            if (punch == null)
            {
                return response;
            }
            else
            {
                response.Message = "Success.....";
                response.Data = punch;

                return response;
            }
        }
    }
}
