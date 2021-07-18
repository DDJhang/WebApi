using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Observable
{
    public class LocationReporter : IObserver<Location>
    {
        public string Name { get { return instName; } }

        private IDisposable unsubscriber;
        private string instName;

        public LocationReporter(string name)
        {
            instName = name;
        }

        public virtual void Subscribe(IObservable<Location> provider)
        {
            if (provider != null)
                unsubscriber = provider.Subscribe(this);
        }

        public virtual void OnCompleted()
        {
            Console.WriteLine("The Location Tracker has completed transmitting data to {0}.", this.Name);
            Unsubscribe();
        }

        public virtual void OnError(Exception error)
        {
            Console.WriteLine("{0}: The location cannot be determined.", this.Name);
        }

        public virtual void OnNext(Location value)
        {
            Console.WriteLine("{2}: The current location is {0}, {1}", value.Latitude, value.Longitude, this.Name);
        }

        public virtual void Unsubscribe()
        {
            unsubscriber.Dispose();
        }
    }
}
