using UnityEngine;
using System.Collections.Generic;
public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private FPSController _playerMovement;
    [SerializeField] private List<Weapon> _weapon;
    private Weapon _currentWeapon;
    private void Start()
    {
        ChangeWeapon(0);
    }
    private void Update()
    {
        CheckWeaponChange();
        if (_currentWeapon != null && _currentWeapon.gameObject.activeSelf)
        {
            CheckShoot();
            CheckReload();
            CheckAutoChange();
            CheckScope();
        }
    }
    private void CheckShoot()
    {
        if (PlayerInput.FireHold) 
            _currentWeapon.TryShoot();
    }
    private void CheckReload()
    {
        if (PlayerInput.Reload) 
            _currentWeapon.TryReload();
    }
    private void CheckAutoChange()
    {
        if (PlayerInput.SwitchFireMode) 
            _currentWeapon.ChangeAuto();
    }
    private void CheckScope()
    {
        if (PlayerInput.Aim) 
            _currentWeapon.Scope();
    }

    private void CheckWeaponChange()
    {
        int keyPressed = PlayerInput.NumberKeyPressed();
        if (keyPressed > 0 && keyPressed <= _weapon.Count)
            ChangeWeapon(keyPressed-1);
    }

    public void ChangeWeapon(int index)
    {
        if (_currentWeapon != null) 
            _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = _weapon[index];
        _currentWeapon.gameObject.SetActive(true);
        _playerMovement.SetSpeed(_currentWeapon.GetMass());
    }
    public void HolsterWeapon()
    {
        if(_currentWeapon != null)
        {
            _currentWeapon.gameObject.SetActive(false);
            _currentWeapon = null;
            _playerMovement.SetSpeed(0);
        }
    }
    public Weapon GetCurrentWeapon() =>
        _currentWeapon;
}
