using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private Transform _camTransform;
    #region Singleton
    public static CameraShake Instance;
    private void Awake() =>
        Instance = this;
    #endregion
    public void ShakeCamera(Vector2 shake)
    {
        int direction = Random.Range(0, 2) > 0 ? 1 : -1;
        // No muliplying by direction and another shake.y to avoid vertical downward shake
        _camTransform.Rotate(Vector3.forward * shake.x * direction + Vector3.right * shake.y);
    }
    public void ShakeCamera(float force)
    {
        float shakeX = Random.Range(-force, force);
        // No muliplying by direction and another shake.y to avoid vertical downward shake
        float shakeY = Random.Range(0, force);
        _camTransform.Rotate(Vector3.forward * shakeX + Vector3.right * shakeY);
    }
}
