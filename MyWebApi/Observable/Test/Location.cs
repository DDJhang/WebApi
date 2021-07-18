namespace MyWebApi.Observable
{
    public struct Location
    {
        public double Latitude { get { return lat; } }
        public double Longitude { get { return lon; } }
        
        private readonly double lat;
        private readonly double lon;

        public Location(double latitude, double longitude)
        {
            lat = latitude;
            lon = longitude;
        }
    }
}
