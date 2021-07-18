using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyWebApi.Observable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //LocationTracker provider = new LocationTracker();

            //provider.Subscribe(data => { Console.WriteLine(data.Latitude); },
            //                   error => { Console.WriteLine("ERROR"); },
            //                   () => { Console.WriteLine("Completed"); }
            //                  );

            //LocationReporter repoter_1 = new LocationReporter("FixedGPS");
            //repoter_1.Subscribe(provider);

            //LocationReporter repoter_2 = new LocationReporter("MobileGPS");
            //repoter_2.Subscribe(provider);

            //provider.TrackLocation(new Location(132, 456));
            //repoter_1.Unsubscribe();
            //provider.TrackLocation(new Location(789, 123));
            //provider.TrackLocation(null);
            //provider.EndTransmission();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
