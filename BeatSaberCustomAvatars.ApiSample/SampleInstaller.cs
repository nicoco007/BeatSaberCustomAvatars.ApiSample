using Zenject;

namespace BeatSaberCustomAvatars.ApiSample
{
    internal class SampleInstaller : Installer
    {
        public override void InstallBindings()
        {
            // single instance & non-lazy so it is created as soon as the scene loads without the need to reference it
            Container.BindInterfacesAndSelfTo<DummyAvatarController>().AsSingle().NonLazy();
        }
    }
}
