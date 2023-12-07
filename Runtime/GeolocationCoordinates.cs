using System;
using UnityEngine.Scripting;

namespace Agava.WebGeolocation
{
    [Serializable]
    public class GeolocationCoordinates
    {
        [field: Preserve]
        public double accuracy;
        [field: Preserve]
        public double? altitude;
        [field: Preserve]
        public double? altitudeAccuracy;
        [field: Preserve]
        public double? heading;
        [field: Preserve]
        public double latitude;
        [field: Preserve]
        public double longitude;
        [field: Preserve]
        public double speed;
    }
}

