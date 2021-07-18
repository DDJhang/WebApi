using System;
using System.Collections.Generic;

namespace MyWebApi.Observable
{
    public class LocationTracker : IObservable<Location>
    {
        private List<IObserver<Location>> observers;

        public LocationTracker()
        {
            observers = new List<IObserver<Location>>();
        }

        public IDisposable Subscribe(IObserver<Location> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);

            return new UnSubscriber(observers, observer);
        }

        public void TrackLocation(Location? loc)
        {
            foreach (var observer in observers)
            {
                if (!loc.HasValue)
                    observer.OnError(new LocationUnknownException());
                else 
                    observer.OnNext(loc.Value);
            }
        }

        public void EndTransmission()
        {
            foreach (var observer in observers)
            {
                if (observers.Contains(observer))
                    observer.OnCompleted();
            }

            observers.Clear();
        }

        private class UnSubscriber : IDisposable
        {
            private List<IObserver<Location>> _observers;
            private IObserver<Location> _observer;

            public UnSubscriber(List<IObserver<Location>> observers, IObserver<Location> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }

    public class LocationUnknownException : Exception
    {
        internal LocationUnknownException() { }
    }
}
