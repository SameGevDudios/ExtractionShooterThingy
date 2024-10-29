
// Script for emulating an effect of a thermal scope with a limited framerate
// Script should be used on a camera with an active render texture

using UnityEngine;
using System.Collections;

public class ThermalScope : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _frameRate;
    private void OnEnable() =>
        StartCoroutine(UpdatingCamera());

    private IEnumerator UpdatingCamera()
    {
        float frameInterval = 1.0f / _frameRate;
        while (true)
        {
            _camera.enabled = true;
            yield return new WaitForEndOfFrame();
            _camera.enabled = false;
            yield return new WaitForSeconds(frameInterval);
        }
    }
}
