using UnityEngine;

public class NewRecoil : MonoBehaviour
{
    [SerializeField] private Transform _spring, _hinge;
    [SerializeField] private Vector2 _movementRecoil, _rotationRecoil;
    [SerializeField] private float _camShakeForce = 0.1f;
    public void ApplyRecoil()
    {
        _spring.localPosition += new Vector3(0, _movementRecoil.y, _movementRecoil.x);
        float sideRecoil = Random.Range(-_rotationRecoil.x, _rotationRecoil.x);
        _hinge.Rotate(Vector3.up * sideRecoil + Vector3.right * _rotationRecoil.y);
        CameraShake.Instance.ShakeCamera(_rotationRecoil * _camShakeForce);
    }
}
