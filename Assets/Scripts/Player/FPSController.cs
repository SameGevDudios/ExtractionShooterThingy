using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private WeaponHandler _weaponHandler;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _cameraTransform, _weaponsTransform;
    [SerializeField] private LayerMask _mask;

    [SerializeField]
    private float _lookSensitivity = 2f, _walkSpeed = 2f, _runSpeed = 5f, _acceleration = 1f,
        _jumpForce = 5f, _maxCarryWeight = 50, 
        _playerTiltAngle = 15, _weaponHorizontalTilt, _weaponVerticalTilt, 
        _playerTiltSpeed, _weaponTiltSpeed;

    private float _verticalLookRotation, _currentSpeed, _currentWeight, _currentPlayerTilt, _currentWeaponTilt;
    private Vector3 _move, _currentVelocity;

    private bool _isRunning, _tiltAim;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _currentSpeed = _walkSpeed;
    }

    private void Update()
    {
        RotateCamera();
        CheckRunInput();
        CheckTiltAim();
        UpdateSpeed();
        MovePlayer();
        ProcessPlayerTilt();
        ProcessWeaponTilt();
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    private void RotateCamera()
    {
        float yRotation = Input.GetAxis("Mouse X") * _lookSensitivity;
        transform.Rotate(Vector3.up * yRotation);

        _verticalLookRotation += Input.GetAxis("Mouse Y") * _lookSensitivity;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90f, 90f);

        _cameraTransform.localEulerAngles = Vector3.left * _verticalLookRotation;
    }
    private void CheckRunInput()
    {
        _isRunning = Input.GetKey(KeyCode.LeftShift);
    }
    private void CheckTiltAim()
    {
        if (Input.GetMouseButtonDown(2))
            _tiltAim = !_tiltAim;
        PlayerStateUI.Instance.SetTiltAimActive(_tiltAim);
    }
    private void UpdateSpeed()
    {
        float ratio = _currentWeight / _maxCarryWeight;
        _currentSpeed = Mathf.Lerp(_isRunning ? _runSpeed : _walkSpeed, 0, ratio);
    }

    private void MovePlayer()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

         _move = transform.right * xMovement + transform.forward * zMovement;
        ProcessInertia();
        _rigidbody.linearVelocity = _currentVelocity + Vector3.up * _rigidbody.linearVelocity.y;
    }
    private void ProcessInertia()
    {
        float multiplier = _move.magnitude < 1 ? 5f : 1f;
        Vector3 currentInput = new Vector3(_move.x * _currentSpeed, 0, _move.z * _currentSpeed);
        _currentVelocity = Vector3.Lerp(_currentVelocity, currentInput, _acceleration * multiplier * Time.deltaTime / _currentWeight);
    }
    private void ProcessPlayerTilt()
    {
        float tiltForce = Input.GetAxis("Tilt");
        //if (tiltForce == 0)
        //    _currentPlayerTilt = Mathf.Lerp(_currentPlayerTilt, 0, _tiltSpeed * Time.deltaTime);
        //else
        //    _currentPlayerTilt = Mathf.Lerp(_currentPlayerTilt, _playerTiltAngle * (tiltForce > 0 ? 1 : -1), _tiltSpeed * Time.deltaTime);
        float newPlayerTilt = tiltForce == 0 ? 0 : _playerTiltAngle * (tiltForce > 0 ? 1 : -1);
        _currentPlayerTilt = Mathf.Lerp(_currentPlayerTilt, newPlayerTilt, _playerTiltSpeed * Time.deltaTime);
        TiltPlayer(_currentPlayerTilt);
    }
    private void ProcessWeaponTilt()
    {
        float tiltForce = Input.GetAxis("Tilt");
        bool scoped = _weaponHandler.GetCurrentWeapon().GetScope();
        Vector3 newPosition = _tiltAim && scoped ? (tiltForce != 0 ? Vector3.right * _weaponHorizontalTilt * (tiltForce > 0 ? -1 : 1) :
            Vector3.down * _weaponVerticalTilt) : Vector3.zero;
        TiltWeapon(Vector3.Lerp(_weaponsTransform.localPosition, newPosition, _weaponTiltSpeed * Time.deltaTime));
    }
    private void TiltPlayer(float newPlayerTilt) =>
        transform.localRotation = Quaternion.Euler(0, transform.localEulerAngles.y, newPlayerTilt);
    private void TiltWeapon(Vector3 newPosition) =>
        _weaponsTransform.localPosition = newPosition;
    private void Jump()
    {
        _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _jumpForce, _rigidbody.linearVelocity.z);
    }
    public void SetSpeed(float weaponMass) =>
        _currentWeight = weaponMass;
    private bool IsGrounded() =>
        Physics.Raycast(transform.position + transform.up * 0.03f, -transform.up, 0.05f, _mask);
}

