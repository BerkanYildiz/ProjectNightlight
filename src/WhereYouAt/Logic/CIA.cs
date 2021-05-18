namespace WhereYouAt.Logic
{
    using System;
    using System.Device.Location;

    internal class CIA
    {
        internal void Locate(string PersonName)
        {
            var GeoWatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            GeoWatcher.MovementThreshold = 0.001d;

            // 
            // Setup a notification routine executed each time the position changes enough (reach specified threshold).
            // 

            GeoWatcher.PositionChanged += (Sender, Args) =>
            {
                var CurrentLocation = Args.Position.Location;
                var Latitude = CurrentLocation.Latitude.ToString("0.0000000000").Replace(',', '.');
                var Longitude = CurrentLocation.Longitude.ToString("0.0000000000").Replace(',', '.');
                var HorizontalAccuracy = CurrentLocation.HorizontalAccuracy.ToString("0.0000000000");
                var VerticalAccuracy = CurrentLocation.VerticalAccuracy.ToString("0.0000000000");

                Console.WriteLine($"[*] Located at '{Latitude}, {Longitude}'. (Accuracy: {HorizontalAccuracy}m/{VerticalAccuracy}m)");
            };

            GeoWatcher.StatusChanged += (Sender, Args) =>
            {
                if (Args.Status == GeoPositionStatus.Initializing ||
                    Args.Status == GeoPositionStatus.NoData)
                {
                    return;
                }

                Console.WriteLine($"[*]  Status -> {Args.Status.ToString()}");
            };

            // 
            // Start to locate the computer.
            // 

            if (!GeoWatcher.TryStart(false, TimeSpan.FromSeconds(40)))
            {
                Console.WriteLine("[*] Failed to start the geolocation.");
                return;
            }

            Console.WriteLine($"[*] Locating '{PersonName}'...");
        }
    }
}
