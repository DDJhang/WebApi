using MyWebApi.Definition;

namespace MyWebApi.Observable
{
    public struct LogMessage
    {
        public LogType Type { get; set; }
        public string Account { get; set; }
        public string Time { get; set; }
        public string Message { get; set; }
    }
}
