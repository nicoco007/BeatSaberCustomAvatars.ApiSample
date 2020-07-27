using CustomAvatar.Zenject;
using IPA;

namespace BeatSaberCustomAvatars.ApiSample
{
    [Plugin(RuntimeOptions.DynamicInit)]
    internal class Plugin
    {
        [Init]
        public Plugin()
        {
            ZenjectHelper.RegisterMenuInstaller<SampleInstaller>();
        }
    }
}
