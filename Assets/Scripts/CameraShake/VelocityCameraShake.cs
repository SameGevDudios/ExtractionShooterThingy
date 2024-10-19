using UnityEngine;

public class VelocityCameraShake : CameraShake
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _vecolityThreshold;
    private float _currentVelocity, _previousVelocity, _impactForce;
    private void Update()
    {
        _currentVelocity = _rigidbody.linearVelocity.y;
        _impactForce = Mathf.Abs(_currentVelocity - _previousVelocity);
        if (_impactForce > _vecolityThreshold)
            ShakeCamera(_impactForce);
        _previousVelocity = _rigidbody.linearVelocity.y;
    }
    private void ShakeCamera(float force) =>
        StartCoroutine(ShakingCamera(Vector2.right * force));
}
