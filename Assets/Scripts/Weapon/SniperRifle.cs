public class SniperRifle : Weapon
{
    private float _startRecoil;
    protected override void Start()
    {
        base.Start();
        _startRecoil = _spread;
    }
    public override void Scope()
    {
        base.Scope();
        _spread = _scoped ? 0 : _startRecoil;
    }
}
