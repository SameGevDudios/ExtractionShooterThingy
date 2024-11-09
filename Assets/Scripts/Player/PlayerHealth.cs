using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] CameraBattery _battery;
    [SerializeField] private int _maxHealth;
    private int _health;
    private void Start()
    {
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
        ImpactCameraShake.Instance.ShakeCamera(ShakeVector());
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
    }
    private Vector2 ShakeVector()
    {
        float xShake = 0.1f;
        float yShake = 0.1f;
        return new Vector2(
            Random.Range(-xShake, xShake),
            Random.Range(-yShake, yShake)
            );

    }
}
