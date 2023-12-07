using AOT;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Agava.WebGeolocation
{
    public static class WebGeolocation
    {
        private static event Action<GeolocationPosition> s_onGetPositionSuccessCallback;
        private static event Action<GeolocationPositionError> s_onGetPositionErrorCallback;

        private static event Action<GeolocationPosition> s_onWatchPositionSuccessCallback;
        private static event Action<GeolocationPositionError> s_onWatchPositionErrorCallback;

        public static bool Supported => IsSupported();

        [DllImport("__Internal")]
        private static extern bool IsSupported();

        #region GetCurrentPosition
        public static void GetCurrentPosition(Action<GeolocationPosition> onSuccessCallback = null, Action<GeolocationPositionError> onErrorCallback = null, PositionOptions options = null)
        {
            s_onGetPositionSuccessCallback = onSuccessCallback;
            s_onGetPositionErrorCallback = onErrorCallback;
            
            GetCurrentPosition(OnGetPositionSuccessCallback, OnGetPositionErrorCallback, JsonUtility.ToJson(options ?? new PositionOptions()));
        }

        [DllImport("__Internal")]
        private static extern void GetCurrentPosition(Action<string> onSuccessCallback, Action<string> onErrorCallback, string options);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPositionSuccessCallback(string positionJson)
        {
            GeolocationPosition entriesResponse = JsonUtility.FromJson<GeolocationPosition>(positionJson);

            s_onGetPositionSuccessCallback?.Invoke(entriesResponse);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnGetPositionErrorCallback(string errorJson)
        {
            GeolocationPositionError entriesResponse = JsonUtility.FromJson<GeolocationPositionError>(errorJson);

            s_onGetPositionErrorCallback?.Invoke(entriesResponse);
        }
        #endregion

        #region WatchPosition
        public static int WatchPosition(Action<GeolocationPosition> onSuccessCallback = null, Action<GeolocationPositionError> onErrorCallback = null, PositionOptions options = null)
        {
            s_onWatchPositionSuccessCallback = onSuccessCallback;
            s_onWatchPositionErrorCallback = onErrorCallback;

            return WatchPosition(OnWatchPositionSuccessCallback, OnWatchPositionErrorCallback, JsonUtility.ToJson(options ?? new PositionOptions()));
        }

        [DllImport("__Internal")]
        private static extern int WatchPosition(Action<string> onSuccessCallback, Action<string> onErrorCallback, string options);

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnWatchPositionSuccessCallback(string positionJson)
        {
            GeolocationPosition entriesResponse = JsonUtility.FromJson<GeolocationPosition>(positionJson);

            s_onWatchPositionSuccessCallback?.Invoke(entriesResponse);
        }

        [MonoPInvokeCallback(typeof(Action<string>))]
        private static void OnWatchPositionErrorCallback(string errorJson)
        {
            GeolocationPositionError entriesResponse = JsonUtility.FromJson<GeolocationPositionError>(errorJson);

            s_onWatchPositionErrorCallback?.Invoke(entriesResponse);
        }
        #endregion

        [DllImport("__Internal")]
        public static extern void ClearWatch(int watchId);
    }
}