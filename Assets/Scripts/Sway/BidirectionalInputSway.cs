using UnityEngine;

public class BidirectionalInputSway : HorizontalInputSway
{
    public BidirectionalInputSway(float swayAmount, float swayDistance, float swaySpeed, IInput input, Transform constraints) :
        base(swayAmount, swayDistance, swaySpeed, input, constraints) { }

    protected override void UpdateSway()
    {
        base.UpdateSway();
        float swayY = Mathf.Clamp(_input.Mouse().y * _swayAmount * Time.deltaTime, -_swayDistance, _swayDistance);
        _targetPosition += Vector3.up * swayY;
    }
}
