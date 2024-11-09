using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private WeaponHandler _weaponHandler;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _cameraTransform, _weaponsTransform;
    [SerializeField] private LayerMask _mask;

    [SerializeField]
    private float _lookSensitivity = 2f, _sneakSpeed = 0.7f, _walkSpeed = 2f, _runSpeed = 5f, _acceleration = 1f,
        _jumpForce = 5f, _maxCarryWeight = 50, 
        _playerTiltAngle = 15, _weaponHorizontalTilt, _weaponVerticalTilt, 
        _playerTiltSpeed, _weaponTiltSpeed;

    private float _verticalLookRotation, _currentSpeed, _currentMaxSpeed, _currentWeight, _currentPlayerTilt;
    private Vector3 _movementDirection, _currentVelocity;

    private bool _tiltAim;
    private void Start()
    {
        _currentSpeed = _walkSpeed;
    }

    private void Update()
    {
        RotateCamera();
        CheckMoveModeInput();
        CheckTiltAim();
        UpdateSpeed();
        CheckInput();
        MovePlayer();
        TilePlayer();
        TiltWeapon();
        if (Input.GetButtonDown("Jump") && IsGrounded())
            Jump();
    }

    private void RotateCamera()
    {
        float yRotation = Input.GetAxis("Mouse X") * _lookSensitivity;
        transform.Rotate(Vector3.up * yRotation);

        _verticalLookRotation += Input.GetAxis("Mouse Y") * _lookSensitivity;
        _verticalLookRotation = Mathf.Clamp(_verticalLookRotation, -90f, 90f);

        _cameraTransform.localEulerAngles = Vector3.left * _verticalLookRotation;
    }
    private void CheckMoveModeInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            _currentMaxSpeed = _runSpeed;
        else if (Input.GetKey(KeyCode.C))
            _currentMaxSpeed = _sneakSpeed;
        else
            _currentMaxSpeed = _walkSpeed;
    }
    private void CheckTiltAim()
    {
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.T))
            _tiltAim = !_tiltAim;
        PlayerStateUI.Instance.SetTiltAimActive(_tiltAim);
    }
    private void UpdateSpeed()
    {
        float ratio = _currentWeight / _maxCarryWeight;
        _currentSpeed = Mathf.Lerp(_currentMaxSpeed, 0, ratio);
    }

    private void MovePlayer()
    {
        ProcessInertia();
        _rigidbody.linearVelocity = _currentVelocity + Vector3.up * _rigidbody.linearVelocity.y;
    }

    private void CheckInput()
    {
        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        _movementDirection = transform.right * xMovement + transform.forward * zMovement;
        // Vertical movement should be calceled due to the tilt mechanic
        _movementDirection.y = 0;
    }

    private void ProcessInertia()
    {
        // Multiply the inertia if no movement keys are held for faster stopping
        float multiplier = _movementDirection.magnitude < 1 ? 5f : 1f;
        _currentVelocity = Vector3.Lerp(_currentVelocity, _movementDirection * _currentSpeed, _acceleration * multiplier * Time.deltaTime / _currentWeight);
    }
    private void TilePlayer()
    {
        float tiltForce = Input.GetAxis("Tilt");
        float newPlayerTilt = tiltForce == 0 ? 0 : _playerTiltAngle * (tiltForce > 0 ? 1 : -1);
        _currentPlayerTilt = Mathf.Lerp(_currentPlayerTilt, newPlayerTilt, _playerTiltSpeed * Time.deltaTime);
        TiltPlayer(_currentPlayerTilt);
    }
    private void TiltWeapon()
    {
        float tiltForce = Input.GetAxis("Tilt");
        bool scoped = _weaponHandler.GetCurrentWeapon().GetScope();
        Vector3 newPosition = _tiltAim && scoped ? (tiltForce != 0 ? (tiltForce > 0 ? -1 : 1) * _weaponHorizontalTilt * Vector3.right :
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

