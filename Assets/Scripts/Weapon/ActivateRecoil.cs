using UnityEngine;

public class ActivateRecoil : MonoBehaviour
{
    [SerializeField] private NewRecoil _recoil;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _recoil.ApplyRecoil();
        }
    }
}
