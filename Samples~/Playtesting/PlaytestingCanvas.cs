using UnityEngine;
using UnityEngine.UI;

namespace Agava.WebGeolocation.Samples.Playtesting
{
    public class PlaytestingCanvas : MonoBehaviour
    {
        [SerializeField] private Button _getCurrentPosition;
        [SerializeField] private Button _watchPosition;
        [SerializeField] private Button _clearPosition;
        [SerializeField] private Text _infoText;

        private int _watchId;

        private void OnEnable()
        {
            _getCurrentPosition.onClick.AddListener(GetCurrentPosition);
            _watchPosition.onClick.AddListener(WatchPosition);
            _clearPosition.onClick.AddListener(ClearPosition);
        }

        private void OnDisable()
        {
            _getCurrentPosition.onClick.RemoveListener(GetCurrentPosition);
            _watchPosition.onClick.RemoveListener(WatchPosition);
            _clearPosition.onClick.RemoveListener(ClearPosition);
        }

        private void GetCurrentPosition()
        {
            WebGeolocation.GetCurrentPosition(
                onSuccessCallback: (position) => _infoText.text = $"GetPosition: {JsonUtility.ToJson(position)}",
                onErrorCallback: (error) => _infoText.text = $"GetPosition: {JsonUtility.ToJson(error)}",
                options: new PositionOptions { timeout = 10000 }
            );
        }

        private void WatchPosition()
        {
            _watchId = WebGeolocation.WatchPosition(
                onSuccessCallback: (position) => _infoText.text = $"WatchPosition: {JsonUtility.ToJson(position)}",
                onErrorCallback: (error) => _infoText.text = $"WatchPosition: {JsonUtility.ToJson(error)}",
                options: new PositionOptions { timeout = 10000 }
            );
        }

        private void ClearPosition()
        {
            WebGeolocation.ClearWatch(_watchId);
        }
    }
}
