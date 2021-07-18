using MyWebApi.Model;
using System.Collections.Generic;

namespace MyWebApi.Response.Punch
{
    public class AttendanceResponse
    {
        public string Message { get; set; }
        public IEnumerable<dynamic> Data { get; set; }
    }
}
