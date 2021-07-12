using MyWebApi.Model;

namespace MyWebApi.Response.Punch
{
    public class AttendanceResponse
    {
        public string Message { get; set; }
        public OneDayPunchModel[] Data { get; set; }
    }
}
