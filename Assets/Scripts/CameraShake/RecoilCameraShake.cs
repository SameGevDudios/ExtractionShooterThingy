using UnityEngine;
public class RecoilCameraShake : CameraShake
{
    #region Singleton
    public static RecoilCameraShake Instance;
    private void Awake() =>
        Instance = this;
    #endregion
    public void ShakeCamera(Vector2 recoil) =>
        Shake(recoil);
}
