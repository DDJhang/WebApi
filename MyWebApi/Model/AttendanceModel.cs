using MyWebApi.Definition;

namespace MyWebApi.Model
{
    public class AttendanceModel
    {
        public string Account { get; set; }
        public AttendanceStatus Status { get; set; }
    }
}
