using UnityEngine;
using Zenject;

public class HorizontalInputSway : IInputSway
{
    protected float _swayAmount, _swayDistance;
    private float _swaySpeed;
    private Vector3 _startPosition;
    protected Vector3 _targetPosition;
    [Inject] protected IInput _input;
    private Transform _constraints;

    public HorizontalInputSway(float swayAmount, float swayDistance, float swaySpeed, IInput input, Transform constraints)
    {
        _swayAmount = swayAmount;
        _swayDistance = swayDistance;
        _swaySpeed = swaySpeed;
        _input = input;
        _constraints = constraints;
        UpdateStartPosition(_constraints.localPosition);
    }
    public void UpdateStartPosition(Vector3 startPosition)
    {
        _startPosition = startPosition;
    }
    public void Update()
    {
        UpdateSway();
        ApplySway();
    }
    protected virtual void UpdateSway()
    {
        float sway = Mathf.Clamp(_input.Mouse().x * _swayAmount * Time.deltaTime, -_swayDistance, _swayDistance);
        _targetPosition = Vector3.right * sway;
    }
    private void ApplySway()
    {
        _constraints.localPosition = Vector3.Lerp(_constraints.localPosition, _startPosition + _targetPosition, _swaySpeed * Time.deltaTime);
    }
}
