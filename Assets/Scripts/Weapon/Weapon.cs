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
    [SerializeField] Recoil _recoilController;

    [Space(1)]
    [Header("Stats")]
    [SerializeField] protected int _ammoPocket, _ammoMax, _damage;
    protected int _ammoCurrent;
    [SerializeField]
    protected float _reloadDuration, _fireRate,
        _spread, _range, _mass;
    private float _cooldown;
    [SerializeField] private bool _canChangeAuto;
    protected bool _auto, _canShoot, _scoped;
    [SerializeField] Transform _gunPoint, _scopePoint, _scopeConstraints;
    Vector3 _startPosition;

    [Space(1)]
    [Header("Inverse Kinematics")]
    [SerializeField] private FastIKFabric _leftArmKinematics;
    [SerializeField] private FastIKFabric _rightArmKinematics;
    [SerializeField] private Transform _leftTarget, _rightTarget;
    private void OnEnable()
    {
        UpdateAmmoText();
        SetIKTargets();
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
        _startPosition = _scopeConstraints.localPosition;
        _canShoot = true;
        UpdateAmmoText();
    }
    private void Update()
    {
        ProcessCooldown();
    }
    private void ProcessCooldown() 
        => _cooldown += Time.deltaTime;
    public void TryShoot()
    {
        if (Input.GetMouseButtonDown(0) || _auto)
        {
            if (_cooldown >= _fireRate)
            {
                if(_ammoCurrent > 0 && _canShoot)
                {
                    _ammoCurrent--;
                    Shoot();
                    AnimateShot();
                    UpdateAmmoText();
                    _recoilController.ApplyRecoil();
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
        if (_canChangeAuto) _auto = !_auto;
    }
    public virtual void Scope()
    {
        _scoped = !_scoped;
        _scopeConstraints.localPosition = _scoped ? _scopePoint.localPosition : _startPosition;
        _scopeConstraints.localEulerAngles = _scoped ? _scopePoint.localEulerAngles : Vector3.zero;
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