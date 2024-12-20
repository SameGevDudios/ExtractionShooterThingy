using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int _pelletsPerShot;
    [SerializeField] private float _reloadInDuration, _reloadOutDuration;
    public override void Shoot()
    {
        for (int i = 0; i < _pelletsPerShot; i++)
            base.Shoot();
    }
    protected override void Reload()
    {
        if(_ammoCurrent < _ammoMax && _ammoPocket > 0)
        {
            _ammoCurrent++;
            _ammoPocket--;
            AnimateReloadBullet();
            UpdateAmmoText();
            Invoke("Reload", _reloadDuration);
        }
        else
        {
            ReloadOut();
        }

    }
    private void AnimateReloadBullet() 
    {
        if (_animator != null) 
            _animator.SetTrigger("ReloadBullet");
    } 
    private void ReloadOut()
    {
        _canShoot = true;
        if(_animator != null)
            _animator.SetTrigger("ReloadOut");
    }
}
