using MyWebApi.Model;
using System.Collections.Generic;

namespace MyWebApi.Response.Punch
{
    public class AttendanceResponse
    {
        public string Message { get; set; }
        public List<OneDayPunchModel> Data { get; set; }
    }
}
