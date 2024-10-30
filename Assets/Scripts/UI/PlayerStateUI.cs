using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    [SerializeField] private Image _scopeActive, _tiltAimActive, _fullAutoActive;
    [SerializeField] private Color _activeColor, _inactiveColor;
    #region Singleton
    public static PlayerStateUI Instance;
    private void Awake() =>
        Instance = this;
    #endregion
    private void Start()
    {
        SetScopeActive(false);
        SetTiltAimActive(false);
        SetFullAutoActive(false);
    }
    public void SetScopeActive(bool active) =>
        _scopeActive.color = active ? _activeColor : _inactiveColor;
    public void SetTiltAimActive(bool active) =>
        _tiltAimActive.color = active ? _activeColor : _inactiveColor;
    public void SetFullAutoActive(bool active) =>
        _fullAutoActive.color = active ? _activeColor : _inactiveColor;
}
