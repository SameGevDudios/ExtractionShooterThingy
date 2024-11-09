using UnityEngine;
public class ImpactCameraShake : CameraShake
{
    #region Singleton
    public static ImpactCameraShake Instance;
    private void Awake() =>
        Instance = this;
    #endregion
    public void ShakeCamera(Vector2 recoil) =>
        Shake(recoil);
}
