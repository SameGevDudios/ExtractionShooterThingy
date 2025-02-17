using UnityEngine;
using Zenject;

public class HorizontalInputSway : IInputSway
{
    private float _swayAmount, _swayDistance, _swaySpeed;
    private Vector3 _startPosition;
    [Inject] private IInput _input;
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
    public void UpdateSway()
    {
        float sway = Mathf.Clamp(_input.Mouse().x * _swayAmount, -_swayDistance, _swayDistance);
        Vector3 targetPosition = Vector3.right * sway;
        _constraints.localPosition = Vector3.Lerp(_constraints.localPosition, _startPosition + targetPosition, _swaySpeed * Time.deltaTime);
    }
}
