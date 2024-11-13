using UnityEngine;

public class SimpleSpring : MonoBehaviour
{
    [SerializeField] private float _springForce, _damping;
    [SerializeField] private Vector3 _targetPosition;
    private Vector3 _velocity;
    private void Update()
    {
        Vector3 springForce = (_targetPosition - transform.localPosition) * _springForce;
        Vector3 dampingForce = _velocity * _damping;
        Vector3 force = springForce * Time.deltaTime - dampingForce;
        _velocity += force;
        transform.localPosition += _velocity;
    }
}
