using UnityEngine;

public class ActivateRecoil : MonoBehaviour
{
    [SerializeField] private Recoil _recoil;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _recoil.ApplyRecoil();
        }
    }
}
