using MyWebApi.Definition;
using MyWebApi.Model;
using MyWebApi.Repository.Interface;
using MyWebApi.Response.Punch;
using MyWebApi.Service.Interface;
using System;
using System.Linq;
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
            var days = (int)model.Status;
            var punch = await _rep.GetPunchListByAccount(model.Account, days);

            if (punch == null)
            {
                return new AttendanceResponse()
                {
                    Message = "FAIL.....",
                    Data = null
                };
            }
            else
            {
                return new AttendanceResponse()
                {
                    Message = "Success.....",
                    Data = punch
                };
            }
        }

        public async Task<AttendanceListResponse> GetAttendanceList(AttendanceStatus status)
        {
            var days = (int)status;
            var punch = await _rep.GetAllPunchList(days);

            var keys = punch.Keys.ToArray();
            var values = punch.Values.ToArray();
            var response = new AttendanceListResponse()
            {
                List = new AttendanceResponse[keys.Length]
            };

            for (int i = 0; i < keys.Length; i++)
            {
                response.List[i] = new AttendanceResponse() {
                    Message = keys[i],
                    Data = values[i]
                };
            }

            return response;
        }
    }
}
