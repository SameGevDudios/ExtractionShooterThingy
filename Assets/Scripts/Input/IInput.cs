using UnityEngine;

public interface IInput
{
    // Movement
    Vector2 Movement();
    bool Run();
    bool Sneak();
    bool Jump();

    // Mouse
    Vector2 Mouse();

    // Tilt
    float Tilt();
    bool SwitchTilt();

    // Weapon
    bool Fire();
    bool FireHold();
    bool Reload();
    bool SwitchFireMode();
    bool Aim();
    int NumberKeyPressed();
}
