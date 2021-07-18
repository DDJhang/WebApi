using System;

namespace MyWebApi.Observable
{
    public interface ILogService : IObservable<LogMessage>
    {
        void WriteLog(LogMessage msg);
        void EndLog();
    }
}
