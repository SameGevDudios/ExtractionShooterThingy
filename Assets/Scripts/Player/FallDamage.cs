using UnityEngine;

public class FallDamage : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _velocityThreshold;
    private float _currentVelocity, _previousVelocity, _impactForce;
    private void Update()
    {
        _currentVelocity = _rigidbody.linearVelocity.y;
        _impactForce = Mathf.Abs(_currentVelocity - _previousVelocity);
        if (_impactForce > _velocityThreshold)
            // ShakeCamera(_impactForce);
        _previousVelocity = _rigidbody.linearVelocity.y;
    }
}
