using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Movement
    public static Vector2 Movement => 
        new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    public static bool Run =>
        Input.GetKey(KeyCode.LeftShift);
    public static bool Sneak =>
        Input.GetKey(KeyCode.C);
    public static bool Jump =>
        Input.GetButtonDown("Jump");

    // Mouse
    public static Vector2 Mouse =>
        new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

    // Tilt
    public static float Tilt =>
        Input.GetAxis("Tilt");
    public static bool SwitchTilt =>
        Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.T);

    // Weapon
    public static bool Fire =>
        Input.GetMouseButton(0);
    public static bool Reload =>
        Input.GetKeyDown(KeyCode.R);
    public static bool SwitchFireMode =>
        Input.GetKeyDown(KeyCode.X);
    public static bool Aim =>
        Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftControl);
    public static int NumberKeyPressed()
    {
        int pressed = -99;
        for (int i = 0; i < 9; i++)
            if (Input.GetKeyDown(i.ToString()))
                pressed = i;
        return pressed;
    }
}
