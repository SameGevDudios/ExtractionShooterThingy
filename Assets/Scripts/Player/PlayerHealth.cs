using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] CameraBattery _battery;
    [SerializeField] private int _maxHealth;
    private int _health;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _health = _maxHealth;
        _battery.SetMaxCharge(_health);
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            GetDamage(10);
        if (Input.GetKeyDown(KeyCode.J))
            Heal(5);
    }
#endif
    public void GetDamage(int damage)
    {
        CameraShake.Instance.ShakeCamera(ShakeVector() * damage);
        _health = Mathf.Max(0, _health - damage);
        _battery.UpdateCharge(_health);
        if(_health == 0)
            Death();
    }
    public void Heal(int amount)
    {
        _health = Mathf.Min(_maxHealth, _health + amount);
        _battery.UpdateCharge(_health);
    }
    private void Death()
    {
        _battery.ActivateWhiteNoise();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private Vector2 ShakeVector()
    {
        float xShake = 0.01f;
        float yShake = 0.01f;
        return new Vector2(
            Random.Range(-xShake, xShake),
            Random.Range(-yShake, yShake)
            );

    }
}
