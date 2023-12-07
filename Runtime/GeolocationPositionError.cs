using System;
using UnityEngine.Scripting;

namespace Agava.WebGeolocation
{
    [Serializable]
    public class GeolocationPositionError
    {
        [field: Preserve]
        public string code;
        [field: Preserve]
        public string message;
    }
}

