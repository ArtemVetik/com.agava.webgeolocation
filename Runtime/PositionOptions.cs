using System;
using UnityEngine.Scripting;

namespace Agava.WebGeolocation
{
    [Serializable]
    public class PositionOptions
    {
        [field: Preserve]
        public bool enableHighAccuracy;
        [field: Preserve]
        public double maximumAge;
        [field: Preserve]
        public double timeout;
    }
}

