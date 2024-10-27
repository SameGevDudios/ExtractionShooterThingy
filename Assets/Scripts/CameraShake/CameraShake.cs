using System.Collections;
using UnityEngine;

public abstract class CameraShake : MonoBehaviour
{
    [SerializeField] protected Transform _camTransform;
    [SerializeField]
    private float _shakeStrength;
    protected void Shake(Vector2 shake)
    {
        int direction = Random.Range(0, 2) > 0 ? 1 : -1;
        float shakeX = _shakeStrength * shake.x * direction;
        // No muliplying by direction and another shake.y to avoid vertical downward shake
        float shakeY = _shakeStrength * shake.y;
        _camTransform.Rotate(Vector3.forward * shakeX + Vector3.right * shakeY);
    }
}
