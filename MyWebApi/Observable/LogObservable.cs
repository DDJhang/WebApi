using System;
using System.Collections.Generic;

namespace MyWebApi.Observable
{
    public class LogObservable : IObservable<LogMessage>
    {
        public List<IObserver<LogMessage>> Observers { get { return _observers; } }

        private List<IObserver<LogMessage>> _observers = null;

        public IDisposable Subscribe(IObserver<LogMessage> observer)
        {
            if (_observers != null && !_observers.Contains(observer))
                _observers.Add(observer);
            return new LogDisposable(_observers, observer);
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
