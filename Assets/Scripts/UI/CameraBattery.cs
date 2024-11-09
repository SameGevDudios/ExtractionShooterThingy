using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CameraBattery : MonoBehaviour
{
    [SerializeField] private GameObject _whiteNoiseDisplay;
    [SerializeField] private CameraTimer _cameraTimer;
    [SerializeField] private Image _batteryImage;
    [SerializeField] private TMP_Text _chargeText;
    private float _maxCharge, _currentCharge;
    public void SetMaxCharge(float maxCharge) =>
        _maxCharge = maxCharge;
    public void UpdateCharge(float newCharge)
    {
        _currentCharge = newCharge / _maxCharge;
        CheckCharge();
        UpdateBatteryImage();
        UpdateChargeText();
    }
    public void ActivateWhiteNoise()
    {
        _whiteNoiseDisplay.SetActive(true);
        _cameraTimer.enabled = false;
        this.enabled = false;
    }
    private void CheckCharge()
    {
        _batteryImage.color = _currentCharge < 0.2f ? Color.red : Color.white;
    }
    private void UpdateBatteryImage() =>
         _batteryImage.fillAmount = _currentCharge;
    private void UpdateChargeText() =>
        _chargeText.text = $"{(_currentCharge * 100).ToString("F0")}%";
}
