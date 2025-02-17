using Zenject;

public class InputInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IInput>().To<DesktopInput>().AsSingle().NonLazy();
    }
}
