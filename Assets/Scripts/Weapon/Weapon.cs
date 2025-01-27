using UnityEngine;
using TMPro;
using DitzelGames.FastIK;

public class Weapon : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera _cam;
    [SerializeField] protected Animator _animator;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private Transform _gunPoint, _scopePoint, _scopeConstraints, _bolt;
    [SerializeField] private Recoil _recoilController;
    [SerializeField] private ParticleSystem _shellParticle;
    private Vector3 _gunStartPosition, _boltStartPosition;
    private IInputSway _inputSway;

    [Space(1)]
    [Header("Stats")]
    [SerializeField] protected int _ammoPocket, _ammoMax, _damage;
    protected int _ammoCurrent;
    [SerializeField]
    protected float _mass, _reloadDuration, _fireRate,
        _spread, _range, _boltOffset,
        _swayAmount, _swayDistance, _swaySpeed;
    private float _cooldown;
    [SerializeField] private bool _canChangeAuto;
    protected bool _auto, _canShoot, _scoped;

    [Space(1)]
    [Header("Inverse Kinematics")]
    [SerializeField] private FastIKFabric _leftArmKinematics;
    [SerializeField] private FastIKFabric _rightArmKinematics;
    [SerializeField] private Transform _leftTarget, _rightTarget;
    private void OnEnable()
    {
        UpdateAmmoText();
        SetIKTargets();
        PlayerStateUI.Instance.SetFullAutoActive(_auto);
    }
    private void OnDisable()
    {
        // Exit scope when changing weapon
        if(_scoped) 
            Scope();
    }
    protected virtual void Start()
    {
        _rigidbody.mass = _mass;
        _ammoCurrent = _ammoMax;
        _cooldown = _fireRate;
        _gunStartPosition = _scopeConstraints.localPosition;
        if(_bolt != null)
            _boltStartPosition = _bolt.localPosition;
        _canShoot = true;
        UpdateAmmoText();

        // Temporary call point
        _inputSway = new HorizontalInputSway(_swayAmount, _swayDistance, _swaySpeed, new DesktopInput(), _scopeConstraints);
    }
    private void Update()
    {
        ProcessCooldown();
        _inputSway.UpdateSway();
    }
    private void ProcessCooldown() =>
        _cooldown += Time.deltaTime;
    public void TryShoot(bool firing)
    {
        if (firing || _auto)
        {
            if (_cooldown >= _fireRate)
            {
                if(_ammoCurrent > 0 && _canShoot)
                {
                    _ammoCurrent--;
                    Shoot();
                    AnimateShot();
                    UpdateAmmoText();
                    MoveBolt();
                    _recoilController.ApplyRecoil();
                    if(_shellParticle != null)
                        _shellParticle.Play();
                }
                _cooldown = 0;
            }
        }
    }
    public void TryReload()
    {
        if (_ammoMax != _ammoCurrent && _ammoPocket > 0 && _canShoot)
        {
            AnimateReload();
            Invoke("Reload", _reloadDuration);
            _canShoot = false;
        }
    }
    public void ChangeAuto()
    {
        if (_canChangeAuto)
        {
            _auto = !_auto;
            PlayerStateUI.Instance.SetFullAutoActive(_auto);
        }
    }
    public virtual void Scope()
    {
        _scoped = !_scoped;
        _scopeConstraints.localPosition = _scoped ? _scopePoint.localPosition : _gunStartPosition;
        _scopeConstraints.localEulerAngles = _scoped ? _scopePoint.localEulerAngles : Vector3.zero;
        PlayerStateUI.Instance.SetScopeActive(_scoped);
        _inputSway.UpdateStartPosition(_scopeConstraints.localPosition);
    }
    public virtual void Shoot()
    {
        GameObject buffer = PoolManager.Instance.InstantiateFromPool("bulletTrail", Vector3.zero, Quaternion.identity);
        TrailFade trail = buffer.GetComponent<TrailFade>();
        RaycastHit hit;
        RandomizeGunPointRotation();
        if (Physics.Raycast(_gunPoint.position, _gunPoint.forward, out hit, _range, _mask))
        {
            Enemy enemy = hit.collider.gameObject.GetComponentInParent<Enemy>();
            if (enemy != null) enemy.GetDamage(_damage);

            trail.SetPositions(_gunPoint.position, hit.point);
            PoolManager.Instance.InstantiateFromPool("BulletImpactParticles", hit.point, Quaternion.LookRotation(hit.normal));
        }
        else
        {
            trail.SetPositions(_gunPoint.position, transform.position + _gunPoint.forward * _range);
        }
    }
    private void RandomizeGunPointRotation()
    {
        if(_spread != 0)
        {
            _gunPoint.localEulerAngles = Vector3.zero;
            float x = Random.Range(-_spread, _spread);
            float y = Random.Range(-_spread, _spread);
            float z = Random.Range(-_spread, _spread);
            _gunPoint.Rotate(new Vector3(x, y, z));
        }
    }
    protected virtual void Reload()
    {
        if (_ammoPocket > _ammoMax)
        {
            _ammoPocket -= _ammoMax - _ammoCurrent;
            _ammoCurrent = _ammoMax;
        }
        else
        {
            _ammoCurrent += _ammoPocket;
            _ammoPocket = 0;
        }
        UpdateAmmoText();
        _canShoot = true;
    }
    private void AnimateShot()
    {
        if(_animator != null) 
            _animator.SetTrigger("Shoot");
    }
    private void AnimateReload()
    {
        if (_animator != null)
            _animator.SetTrigger("Reload");
    }
    protected void UpdateAmmoText()
    {
        if(_ammoText != null)
            _ammoText.text = $"{_ammoCurrent}/{_ammoPocket}";
    }
    private void MoveBolt()
    {
        if (_bolt != null)
            _bolt.localPosition = _boltStartPosition + Vector3.forward * _boltOffset;
    }
    private void SetIKTargets()
    {
        _leftArmKinematics.Target = _leftTarget;
        _rightArmKinematics.Target = _rightTarget;
    }
    public float GetMass() => 
        _mass;
    public bool GetScope() =>
        _scoped;

}