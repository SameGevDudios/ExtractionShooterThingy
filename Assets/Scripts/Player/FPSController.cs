using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private LayerMask _mask;

    [SerializeField] private float _lookSensitivity = 2f, _moveSpeed = 5f, _acceleration = 1,
        _jumpForce = 5f, _maxCarryWeight = 50, _tiltAngle = 15, _tiltSpeed = 1;

    private float _verticalLookRotation, _currentSpeed, _currentWeight, _currentTilt;
    private Vector3 _currentInput, _currentVelocity;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        _currentSpeed = _moveSpeed;
    }

    private void Update()
    {
        RotateCamera();
        MovePlayer();
        CheckTilt();
        TiltPlayer();
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

    private void MovePlayer()
    {
        float xMovement = Input.GetAxis("Horizontal") * _currentSpeed;
        float zMovement = Input.GetAxis("Vertical") * _currentSpeed;

        Vector3 move = transform.right * xMovement + transform.forward * zMovement;
        _currentInput = new Vector3(move.x, _rigidbody.velocity.y, move.z);
        _currentVelocity = new Vector3(_currentVelocity.x, _rigidbody.velocity.y, _currentVelocity.z);
        ProcessInertia();
        _rigidbody.velocity = _currentVelocity;
    }
    private void ProcessInertia()
    {
        _currentVelocity = Vector3.Lerp(_currentVelocity, _currentInput, _acceleration / _currentWeight);
    }
    private void CheckTilt()
    {
        float tiltForce = Input.GetAxis("Tilt");
        if (tiltForce == 0)
            _currentTilt = Mathf.Lerp(_currentTilt, 0, _tiltSpeed * Time.deltaTime);
        else
            _currentTilt = Mathf.Lerp(_currentTilt, _tiltAngle * (tiltForce > 0 ? 1 : -1), _tiltSpeed * Time.deltaTime);
    }
    private void TiltPlayer()
    {
        transform.localRotation = Quaternion.Euler(0, transform.localEulerAngles.y, _currentTilt);
    }
    private void Jump()
    {
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _jumpForce, _rigidbody.velocity.z);
    }
    public void SetSpeed(float weaponMass)
    {
        float ratio = weaponMass / _maxCarryWeight;
        _currentSpeed = Mathf.Lerp(_moveSpeed, 0, ratio);
        _currentWeight = weaponMass;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + transform.up * 0.03f, -transform.up, 0.05f, _mask);
    }
}

