using UnityEngine;

public class InputBootstrapper : MonoBehaviour
{
    [SerializeField] private FPSController _movement;
    [SerializeField] private WeaponHandler _weaponHandler;

    private void Awake()
    {
        IInput input = new DesktopInput();
        _movement.AssignInput(input);
        _weaponHandler.AssignInput(input);
    }
}
