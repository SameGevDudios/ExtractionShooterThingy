using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform _camTransform;
    [SerializeField] private float _shakeStrength;
    #region Singleton
    public static CameraShake Instance;
    private void Awake() =>
        Instance = this;
    #endregion
    public void ShakeCamera(Vector2 shake)
    {
        int direction = Random.Range(0, 2) > 0 ? 1 : -1;
        float shakeX = _shakeStrength * shake.x * direction;
        // No muliplying by direction and another shake.y to avoid vertical downward shake
        float shakeY = _shakeStrength * shake.y;
        _camTransform.Rotate(Vector3.forward * shakeX + Vector3.right * shakeY);
    }
    public void ShakeCamera(float force)
    {
        float shakeX = _shakeStrength * Random.Range(-force, force);
        // No muliplying by direction and another shake.y to avoid vertical downward shake
        float shakeY = _shakeStrength * Random.Range(0, force);
        _camTransform.Rotate(Vector3.forward * shakeX + Vector3.right * shakeY);
    }
}
