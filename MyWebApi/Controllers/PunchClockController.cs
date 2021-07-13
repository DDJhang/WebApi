using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using MyWebApi.Model;
using MyWebApi.Response.Punch;
using MyWebApi.Service.Interface;
using System;
using System.Threading.Tasks;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PunchClockController : ControllerBase
    {
        private IPunchClockService _service;

        private string _checkTableSqlCmd = "Data Source=localhost;port=3306;User ID=root; Password=123456; Persist Security Info=yes";

        public PunchClockController(IPunchClockService service)
        {
            _service = service;
        }

        [HttpPost("CreatePunchTable")]
        public ActionResult<string> CrreateDB()
        {
            var tableName = Method.DateTimeToTableName(DateTime.Now);
            if (Method.CheckTableExist(_checkTableSqlCmd, tableName))
                return "Already TABLE => " + tableName;

            var strCmd = "CREATE TABLE " + tableName + " (account CHAR  NOT NULL, name CHAR  NOT NULL, punchin CHAR  NOT NULL, punchout CHAR  NOT NULL, PRIMARY KEY (account));";

            var connectString = "Database=account; Data Source=127.0.0.1; User Id=root; port=3306; Password=123456;";
            using (var connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand(strCmd, connection))
                {
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    finally
                    {
                        connection.Close();

                        cmd.Dispose();
                        connection.Dispose();
                    }
                }
            }

            return "Created TABLE => " + tableName;
        }


        [HttpPost("PunchClock")]
        public async Task<ActionResult<PunchResponse>> PunchClock(PunchModel model)
        {
            var tableName = Method.DateTimeToTableName(DateTime.Now);
            if (!Method.CheckTableExist(_checkTableSqlCmd, tableName))
            {
                return new PunchResponse()
                {
                    Message = "打卡失敗(Invalid Table)..."
                };
            }

            return await _service.PunchClock(model);
        }

        [HttpGet("GetAttendance")]
        public async Task<ActionResult<AttendanceResponse>> GetAttendance(AttendanceModel model)
        {
            var tableName = Method.DateTimeToTableName(DateTime.Now);
            if (!Method.CheckTableExist(_checkTableSqlCmd, tableName))
            {
                return new AttendanceResponse()
                {
                    Message = "獲取失敗(Invalid Table)...",
                    Data = null
                };
            }

            return await _service.GetAttendance(model);
        }
    }
}
