using MyWebApi.Repository;
using System;
using System.Collections.Generic;

namespace MyWebApi.Observable
{
    public class LogService : ILogService
    {
        private IAccountRepository _resp;

        private List<IObserver<LogMessage>> _observers = null;

        public LogService(IAccountRepository resp)
        {
            _resp = resp;
            _observers = new List<IObserver<LogMessage>>();
        }

        public IDisposable Subscribe(IObserver<LogMessage> observer)
        {
            if (_observers != null && !_observers.Contains(observer))
                _observers.Add(observer);
            return new LogDisposable(_observers, observer);
        }

        public void WriteLog(LogMessage msg)
        {
            var hadAccount = _resp.GetPlayerByAccount(msg.Account) != null;

            for (int i = 0; i < _observers.Count; i++)
            {
                if(hadAccount)
                    _observers[i].OnNext(msg);
                else
                    _observers[i].OnError(new Exception("Invalid Account: " + msg.Account));
            }
        }

        public void EndLog()
        {
            for (int i = 0; i < _observers.Count; i++)
                _observers[i].OnCompleted();

            _observers.Clear();
        }
    }

    public class LogDisposable : IDisposable
    {
        private List<IObserver<LogMessage>> _observers = null;
        private IObserver<LogMessage> _observer = null;

        public LogDisposable(List<IObserver<LogMessage>> observers, IObserver<LogMessage> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers != null && _observers.Contains(_observer))
                _observers.Remove(_observer);

            _observers = null;
            _observer = null;
        }
    }
}
