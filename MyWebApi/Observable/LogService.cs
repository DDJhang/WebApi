using MyWebApi.Repository;
using System;

namespace MyWebApi.Observable
{
    public class LogService : ILogService
    {
        private IAccountRepository _resp;

        private LogObservable _observable;

        public LogService(IAccountRepository resp, LogObservable observable, LogObserver logserver)
        {
            _resp = resp;
            _observable = observable;

            _observable.Subscribe(logserver);
        }

        public void WriteLog(LogMessage msg)
        {
            var hadAccount = _resp.GetPlayerByAccount(msg.Account) != null;

            for (int i = 0; i < _observable.Observers.Count; i++)
            {
                if(hadAccount)
                    _observable.Observers[i].OnNext(msg);
                else
                    _observable.Observers[i].OnError(new Exception("Invalid Account: " + msg.Account));
            }
        }

        public void EndLog()
        {
            for (int i = 0; i < _observable.Observers.Count; i++)
                _observable.Observers[i].OnCompleted();

            _observable.Observers.Clear();
        }
    }
}
