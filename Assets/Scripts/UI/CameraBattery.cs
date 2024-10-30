using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraBattery : MonoBehaviour
{
    [SerializeField] private GameObject _whiteNoiseDisplay;
    [SerializeField] private CameraTimer _cameraTimer;
    [SerializeField] private Image _batteryImage;
    [SerializeField] private TMP_Text _chargeText;
    [Tooltip("Seconds untill discharge")]
    [SerializeField] private float _batteryCapacity;
    private float _maxBatteryCapacity, _currentCharge;
    private bool _chargeSufficient = true;
    private void Start() =>
        _maxBatteryCapacity = _batteryCapacity;

    private void Update()
    {
        _currentCharge = _batteryCapacity / _maxBatteryCapacity;
        CheckCharge();
        CheckBattery();
    }
    private void CheckCharge()
    {
        if (_chargeSufficient && _currentCharge < 0.2f)
        {
            _batteryImage.color = Color.red;
            _chargeSufficient = false;
        }
    }
    private void CheckBattery()
    {
        if (_batteryCapacity > 0)
        {
            _batteryCapacity -= Time.deltaTime;
            UpdateBatteryImage();
            UpdateChargeText();
        }
        else
        {
            _whiteNoiseDisplay.SetActive(true);
            _cameraTimer.enabled = false;
            this.enabled = false;
        }
    }
    private void UpdateBatteryImage() =>
         _batteryImage.fillAmount = _currentCharge;
    private void UpdateChargeText() =>
        _chargeText.text = $"{(_currentCharge * 100).ToString("F0")}%";
}
