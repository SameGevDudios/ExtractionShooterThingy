using UnityEngine;

public class FallDamage : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _shakeForce, _fallDamageForce, 
        _shakeVelocityThreshold, _fallDamageThreshold;
    private float _currentVelocity, _previousVelocity, _impactForce, _fallTime;
    private void Update()
    {
        ProcessCameraShake();
        ProcessFallDamage();
    }
    private void ProcessCameraShake()
    {
        _currentVelocity = _rigidbody.linearVelocity.y;
        _impactForce = Mathf.Abs(_currentVelocity - _previousVelocity);
        if (_impactForce > _shakeVelocityThreshold)
            CameraShake.Instance.ShakeCamera(_shakeForce * _impactForce);
        _previousVelocity = _rigidbody.linearVelocity.y;
    }
    private void ProcessFallDamage()
    {
        if(_rigidbody.linearVelocity.y < _fallDamageThreshold)
        {
            _fallTime += Time.deltaTime;
        }
        else
        {
            if(_fallTime > 0)
            {
                int damage = (int)(_fallTime * _fallTime * _fallDamageForce);
                print($"Damage: {damage}");
                if(damage > 0)
                    _playerHealth.GetDamage(damage);
                _fallTime = 0;
            }
        }
    }
}
