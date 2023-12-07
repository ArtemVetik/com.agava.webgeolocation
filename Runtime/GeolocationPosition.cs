using System;
using UnityEngine.Scripting;

namespace Agava.WebGeolocation
{
    [Serializable]
    public class GeolocationPosition
    {
        [field: Preserve]
        public int watchId;
        [field: Preserve]
        public GeolocationCoordinates coords;
        [field: Preserve]
        public long timestamp;
    }
}

