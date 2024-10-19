using System.Collections;
using UnityEngine;

public abstract class CameraShake : MonoBehaviour
{
    [SerializeField] protected Transform _camTransform;
    [SerializeField]
    private float _shakeStrength, _shakeDuration;
    protected IEnumerator ShakingCamera(Vector2 shake)
    {
        // If random return 1 direction is also 1, if random returns 0 direction is -1
        int direction = Random.Range(0, 2) > 0 ? 1 : -1;
        float shakeStepX = _shakeStrength * shake.x * shake.x * direction * Time.deltaTime / _shakeDuration;
        // No muliplying by direction and another shake.y to avoid vertical downward shake
        float shakeStepZ = _shakeStrength * shake.y * Time.deltaTime / _shakeDuration; 
        float timer = _shakeDuration;
        print($"shakeStepZ: {shakeStepZ}");
        while (timer > 0)
        {
            _camTransform.Rotate(Vector3.forward * shakeStepX + Vector3.right * shakeStepZ);
            timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
