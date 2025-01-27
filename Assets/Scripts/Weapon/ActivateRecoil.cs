using UnityEngine;

public class ActivateRecoil : MonoBehaviour
{
    [SerializeField] private NewRecoil _recoil;
    private IInput _input;
    private void Start()
    {
        _input = new DesktopInput();
    }
    private void Update()
    {
        if (_input.FireHold())
        {
            _recoil.ApplyRecoil();
        }
    }
}
