using UnityEngine;

public class MovementSway : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _velocityThreshold, _idleSpeed, _swaySpeed,
        _horizontalSwayDistance, _verticalSwayDistance, _verticalSharpness;
    private float _function;
    private int direction = 1;
    private void Update() =>
        ProcessMovement(_rigidbody.linearVelocity, _rigidbody.linearVelocity.magnitude);
    private void ProcessMovement(Vector3 movement, float baseSpeed)
    {
        if (movement.magnitude > _velocityThreshold)
        {
            _function += Mathf.PI * _swaySpeed * baseSpeed * direction * Time.deltaTime;
            if (_function < 0 || _function > Mathf.PI)
                direction *= 1;
            ApplySway();
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, _idleSpeed * Time.deltaTime);
            _function = 0;
        }
    }
    private void ApplySway()
    {
        transform.localPosition = Vector3.right * Mathf.Cos(_function) * _horizontalSwayDistance +
            Vector3.up * Mathf.Pow(Mathf.Sin(_function + Mathf.PI), _verticalSharpness) * _verticalSwayDistance;
    }
}
