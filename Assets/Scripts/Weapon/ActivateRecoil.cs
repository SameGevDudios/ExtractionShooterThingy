using UnityEngine;

public class ActivateRecoil : MonoBehaviour
{
    [SerializeField] private NewRecoil _recoil;
    private void Update()
    {
        if (PlayerInput.FireHold)
        {
            _recoil.ApplyRecoil();
        }
    }
}
