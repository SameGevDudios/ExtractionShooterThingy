using UnityEngine;

public class Recoil : MonoBehaviour
{
    [SerializeField] private Rigidbody _gunRigidbody;
    [SerializeField] private Vector2 _movementRecoil, _rotationRecoil;
    [SerializeField] private float _camShakeForce = 0.1f;
    public void ApplyRecoil()
    {
        _gunRigidbody.AddForce(transform.forward * _movementRecoil.x + transform.up * _movementRecoil.y, ForceMode.Impulse);
        float sideRecoil = Random.Range(-_rotationRecoil.x, _rotationRecoil.x);
        _gunRigidbody.transform.Rotate(Vector3.up * sideRecoil + Vector3.right * _rotationRecoil.y);
        CameraShake.Instance.ShakeCamera(_rotationRecoil * _camShakeForce);
    }
}
