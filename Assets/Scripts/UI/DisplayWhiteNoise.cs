using UnityEngine;
using UnityEngine.UI;

public class DisplayWhiteNoise : MonoBehaviour
{
    [SerializeField] private Image _noiseDisplay;
    [SerializeField] private Sprite[] _noiseSprite;
    [SerializeField] private float _framerate;
    private float _cooldown, _timer;
    private int _currentFrame;
    private void Start()
    {
        _cooldown = 1.0f / _framerate;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _cooldown)
        {
            UpdateSprite();
            _timer = 0;
        }
    }
    private void UpdateSprite()
    {
        _currentFrame = _currentFrame == _noiseSprite.Length - 1 ? 0 : _currentFrame + 1;
        _noiseDisplay.sprite = _noiseSprite[_currentFrame];
    }
}
