using UnityEngine;

public class DesktopInput : IInput
{
    // Movement
    public Vector2 Movement() => 
        new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    public bool Run() =>
        Input.GetKey(KeyCode.LeftShift);
    public bool Sneak() =>
        Input.GetKey(KeyCode.C);
    public bool Jump() =>
        Input.GetButtonDown("Jump");

    // Mouse
    public Vector2 Mouse() =>
        new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    // Tilt
    public float Tilt() =>
        Input.GetAxis("Tilt");
    public bool SwitchTilt() =>
        Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.T);

    // Weapon
    public bool Fire() =>
        Input.GetMouseButtonDown(0);
    public bool FireHold() =>
        Input.GetMouseButton(0);
    public bool Reload() =>
        Input.GetKeyDown(KeyCode.R);
    public bool SwitchFireMode() =>
        Input.GetKeyDown(KeyCode.X);
    public bool Aim() =>
        Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftControl);
    public int NumberKeyPressed()
    {
        int pressed = -99;
        for (int i = 0; i < 9; i++)
            if (Input.GetKeyDown(i.ToString()))
                pressed = i;
        return pressed;
    }
}
