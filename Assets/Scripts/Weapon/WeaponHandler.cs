using UnityEngine;
using System.Collections.Generic;
public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private FPSController _playerMovement;
    [SerializeField] private List<Weapon> _weapon;
    [SerializeField] private List<MovementSway> _movementSway;
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
        if (Input.GetMouseButton(0)) 
            _currentWeapon.TryShoot();
    }
    private void CheckReload()
    {
        if (Input.GetKeyDown(KeyCode.R)) 
            _currentWeapon.TryReload();
    }
    private void CheckAutoChange()
    {
        if (Input.GetKeyDown(KeyCode.X)) 
            _currentWeapon.ChangeAuto();
    }
    private void CheckScope()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftControl)) 
            _currentWeapon.Scope();
    }

    private void CheckWeaponChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            ChangeWeapon(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            ChangeWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            ChangeWeapon(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            ChangeWeapon(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            ChangeWeapon(4);
        else if (Input.GetKeyDown(KeyCode.H))
            HolsterWeapon();
    }

    public void ChangeWeapon(int index)
    {
        if (_currentWeapon != null) 
            _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = _weapon[index];
        _currentWeapon.gameObject.SetActive(true);
        _playerMovement.SetMovementSway(_movementSway[index]);
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
