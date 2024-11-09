using UnityEngine;

public class SimpleSpring : MonoBehaviour
{
    [SerializeField] private float _springForce, _damping;
    [SerializeField] private Vector3 targetPosition = new Vector3(0, 0, 0);
    private Vector3 velocity;
    void Update()
    {
        Vector3 springForce = (targetPosition - transform.localPosition) * _springForce;
        Vector3 dampingForce = velocity * _damping;
        Vector3 force = springForce - dampingForce;
        velocity += force * Time.deltaTime;
        transform.localPosition += velocity * Time.deltaTime;
    }
}
