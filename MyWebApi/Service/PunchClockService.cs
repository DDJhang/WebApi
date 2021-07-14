using MyWebApi.Definition;
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

            var punchType = PunchType.PunchIn;
            string tableName = Method.DateTimeToTableName(DateTime.Now);
            string time = Method.DateTimeToPunchString(DateTime.Now);
            if (punch == null)
            {
                var punchModel = new OneDayPunchModel()
                {
                    Account = model.Account,
                    Name = model.Name,
                    PunchIn = time,
                    PunchOut = "-"
                };
                await _rep.Add(punchModel, tableName);
            }
            else
            {
                punch.PunchOut = time;
                punchType = PunchType.PunchOut;
                await _rep.Update(punch, tableName, PunchType.PunchOut);
            }

            var response = new PunchResponse()
            {
                Message = "打卡成功... " + punchType.ToString() + " => " + time
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
