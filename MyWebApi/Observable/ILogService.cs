using System;

namespace MyWebApi.Observable
{
    public interface ILogService
    {
        void WriteLog(LogMessage msg);
        void EndLog();
    }
}
