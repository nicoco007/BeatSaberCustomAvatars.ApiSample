using Zenject;

namespace CustomAvatar.ApiSample
{
    internal class SampleInstaller : Installer
    {
        public override void InstallBindings()
        {
            // single instance & non-lazy so it is created as soon as the scene loads without the need to reference it
            Container.BindInterfacesAndSelfTo<AvatarController>().AsSingle().NonLazy();
        }
    }
}
